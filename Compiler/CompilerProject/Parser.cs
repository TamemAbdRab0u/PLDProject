using System;
using System.Collections.Generic;

namespace CompilerProject
{
    public class Node
    {
        public string Name { get; set; }
        public List<Node> Children { get; set; } = new List<Node>();

        public Node(string name) 
        {
            Name = name; 
        }
        public void Add(Node node)
        {
            if (node != null) Children.Add(node); 
        }
    }

    public class Parser
    {
        private List<Token> _tokens;
        private int _index;
        public List<string> Errors { get; } = new List<string>();

        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
            _index = 0;
        }

        private Token Current => _index < _tokens.Count ? _tokens[_index] : _tokens[_tokens.Count - 1];

        private Token Match(TokenType type)
        {
            if (Current.Type == type)
            {
                var t = Current;
                _index++;
                return t;
            }
            Errors.Add($"Expected {type} but found {Current.Type} ('{Current.Value}') at position {Current.Position}");
            return null;
        }

        private Token MatchValue(string val)
        {
            if (Current.Value == val)
            {
                var t = Current;
                _index++;
                return t;
            }
            Errors.Add($"Expected '{val}' but found '{Current.Value}' at position {Current.Position}");
            return null;
        }

        public Node Parse() {return Program();}

        private Node Program()
        {
            var node = new Node("Program");
            if (MatchValue("Start") != null)
            {
                node.Add(StmtList());
                MatchValue("End");
            }
            return node;
        }

        private Node StmtList()
        {
            var node = new Node("StmtList");
            node.Add(Concept());
            
            if (Current.Value != "End" && Current.Value != "~>" && Current.Type != TokenType.EndOfFile && 
                Current.Value != "case" && Current.Value != "default" &&
                Current.Value != "ef" && Current.Value != "else")
            {
                node.Add(StmtList());
            }
            return node;
        }

        private Node Concept()
        {
            var node = new Node("Concept");
            if (Current.Type == TokenType.Identifier) node.Add(Assign());
            else if (Current.Value == "if") node.Add(IfStmt());
            else if (Current.Value == "for") node.Add(ForStmt());
            else if (Current.Value == "switch") node.Add(SwitchStmt());
            else
            {
                Errors.Add($"Unexpected concept start '{Current.Value}' at {Current.Position}");
                _index++;
            }
            return node;
        }


        private Node Assign()
        {
            var node = new Node("Assign");
            node.Add(new Node(Match(TokenType.Identifier)?.Value));
            Match(TokenType.Assign);
            node.Add(Expr());
            return node;
        }

        private Node Expr()
        {
            var node = new Node("Expr");
            node.Add(Term());
            node.Add(ExprPrime());
            return node;
        }

        private Node ExprPrime()
        {
            if (Current.Value == "+" || Current.Value == "-")
            {
                var node = new Node("ExprPrime");
                node.Add(new Node(Match(TokenType.ArithmeticOperator).Value));
                node.Add(Term());
                node.Add(ExprPrime());
                return node;
            }
            return null;
        }

        private Node Term()
        {
            var node = new Node("Term");
            node.Add(Factor());
            node.Add(TermPrime());
            return node;
        }

        private Node TermPrime()
        {
            if (Current.Value == "*" || Current.Value == "/" || Current.Value == "%")
            {
                var node = new Node("TermPrime");
                node.Add(new Node(Match(TokenType.ArithmeticOperator).Value));
                node.Add(Factor());
                node.Add(TermPrime());
                return node;
            }
            return null;
        }

        private Node Factor()
        {
            var node = new Node("Factor");
            node.Add(Primary());
            node.Add(FactorPrime());
            return node;
        }

        private Node FactorPrime()
        {
            if (Current.Value == "**")
            {
                var node = new Node("FactorPrime");
                node.Add(new Node(Match(TokenType.ArithmeticOperator).Value));
                node.Add(Primary());
                node.Add(FactorPrime());
                return node;
            }
            return null;
        }

        private Node Primary()
        {
            var node = new Node("Primary");
            if (Current.Value == "(")
            {
                MatchValue("(");
                node.Add(Expr());
                MatchValue(")");
            }
            else if (Current.Type == TokenType.Identifier)
            {
                node.Add(new Node(Match(TokenType.Identifier).Value));
            }
            else
            {
                node.Add(new Node(Match(TokenType.Number)?.Value));
            }
            return node;
        }



        private Node IfStmt()
        {
            var node = new Node("IfStmt");
            MatchValue("if");
            MatchValue("(");
            node.Add(Cond());
            MatchValue(")");
            Match(TokenType.Colon);
            MatchValue("<~");
            node.Add(StmtList());
            MatchValue("~>");
            node.Add(ECond());
            return node;
        }

        private Node ECond()
        {
            if (Current.Value == "ef")
            {
                var node = new Node("EfStmt");
                MatchValue("ef");
                MatchValue("(");
                node.Add(Cond());
                MatchValue(")");
                Match(TokenType.Colon);
                MatchValue("<~");
                node.Add(StmtList());
                MatchValue("~>");
                node.Add(ECond());
                return node;
            }
            else if (Current.Value == "else")
            {
                var node = new Node("ElseStmt");
                MatchValue("else");
                Match(TokenType.Colon);
                MatchValue("<~");
                node.Add(StmtList());
                MatchValue("~>");
                return node;
            }
            return null;
        }

        private Node Cond()
        {
            var node = new Node("Cond");
            node.Add(Expr());
            if (Current.Type == TokenType.Operator)
            {
                node.Add(new Node(Match(TokenType.Operator).Value));
                node.Add(Expr());
            }
            return node;
        }


        private Node ForStmt()
        {
            var node = new Node("ForStmt");
            MatchValue("for");
            node.Add(new Node(Match(TokenType.Identifier)?.Value));
            MatchValue("in");
            node.Add(RangeExpr());
            Match(TokenType.Colon);
            MatchValue("<~");
            node.Add(StmtList());
            MatchValue("~>");
            return node;
        }

        private Node RangeExpr()
        {
            var node = new Node("RangeExpr");
            MatchValue("range");
            MatchValue("(");
            node.Add(Expr());
            if (Current.Type == TokenType.Comma)
            {
                Match(TokenType.Comma);
                node.Add(Expr());
            }
            MatchValue(")");
            return node;
        }


        private Node SwitchStmt()
        {
            var node = new Node("SwitchStmt");
            MatchValue("switch");
            MatchValue("(");
            node.Add(Expr());
            MatchValue(")");
            Match(TokenType.Colon);
            MatchValue("<~");
            node.Add(CaseList());
            MatchValue("~>");
            return node;
        }

        private Node CaseList()
        {
            var node = new Node("CaseList");
            if (Current.Value == "case")
            {
                node.Add(Case());
                node.Add(CaseList());
            }
            else if (Current.Value == "default")
            {
                node.Add(DefaultCase());
            }
            return node;
        }

        private Node Case()
        {
            var node = new Node("Case");
            MatchValue("case");
            MatchValue("(");
            if (Current.Type == TokenType.Identifier)
                node.Add(new Node(Match(TokenType.Identifier).Value));
            else
                node.Add(new Node(Match(TokenType.Number).Value));
            MatchValue(")");
            Match(TokenType.Colon);
            node.Add(StmtList());
            return node;
        }

        private Node DefaultCase()
        {
            var node = new Node("DefaultCase");
            MatchValue("default");
            Match(TokenType.Colon);
            node.Add(StmtList());
            return node;
        }
    }
}
