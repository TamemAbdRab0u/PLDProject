
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF          =  0, // (EOF)
        SYMBOL_ERROR        =  1, // (Error)
        SYMBOL_WHITESPACE   =  2, // Whitespace
        SYMBOL_MINUS        =  3, // '-'
        SYMBOL_EXCLAMEQ     =  4, // '!='
        SYMBOL_PERCENT      =  5, // '%'
        SYMBOL_LPAREN       =  6, // '('
        SYMBOL_RPAREN       =  7, // ')'
        SYMBOL_TIMES        =  8, // '*'
        SYMBOL_TIMESTIMES   =  9, // '**'
        SYMBOL_COMMA        = 10, // ','
        SYMBOL_DIV          = 11, // '/'
        SYMBOL_COLON        = 12, // ':'
        SYMBOL_PLUS         = 13, // '+'
        SYMBOL_LT           = 14, // '<'
        SYMBOL_EQ           = 15, // '='
        SYMBOL_EQEQ         = 16, // '=='
        SYMBOL_GT           = 17, // '>'
        SYMBOL_CASE         = 18, // case
        SYMBOL_DEFAULT      = 19, // default
        SYMBOL_DIGIT        = 20, // Digit
        SYMBOL_EF           = 21, // ef
        SYMBOL_ELSE         = 22, // else
        SYMBOL_END          = 23, // End
        SYMBOL_FOR          = 24, // for
        SYMBOL_ID           = 25, // Id
        SYMBOL_IF           = 26, // if
        SYMBOL_IN           = 27, // in
        SYMBOL_NEWLINE      = 28, // NewLine
        SYMBOL_RANGE        = 29, // range
        SYMBOL_START        = 30, // Start
        SYMBOL_SWITCH       = 31, // switch
        SYMBOL_ASSIGN       = 32, // <assign>
        SYMBOL_CASE2        = 33, // <case>
        SYMBOL_CASE_LIST    = 34, // <case_list>
        SYMBOL_CONCEPT      = 35, // <concept>
        SYMBOL_COND         = 36, // <cond>
        SYMBOL_DEFAULT_CASE = 37, // <default_case>
        SYMBOL_DIGIT2       = 38, // <digit>
        SYMBOL_E_COND       = 39, // <e_cond>
        SYMBOL_EXPR         = 40, // <expr>
        SYMBOL_FACTOR       = 41, // <factor>
        SYMBOL_FOR_STMT     = 42, // <for_stmt>
        SYMBOL_ID2          = 43, // <id>
        SYMBOL_IF_STMT      = 44, // <if_stmt>
        SYMBOL_NEWLINE2     = 45, // <NewLine>
        SYMBOL_OP           = 46, // <op>
        SYMBOL_PRIMARY      = 47, // <primary>
        SYMBOL_PROGRAM      = 48, // <program>
        SYMBOL_RANGE_EXPR   = 49, // <range_expr>
        SYMBOL_STMT_LIST    = 50, // <stmt_list>
        SYMBOL_SWITCH_STMT  = 51, // <switch_stmt>
        SYMBOL_TERM         = 52  // <term>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM_START_END                                =  0, // <program> ::= Start <stmt_list> End
        RULE_STMT_LIST                                        =  1, // <stmt_list> ::= <concept>
        RULE_STMT_LIST2                                       =  2, // <stmt_list> ::= <concept> <stmt_list>
        RULE_CONCEPT                                          =  3, // <concept> ::= <assign>
        RULE_CONCEPT2                                         =  4, // <concept> ::= <if_stmt>
        RULE_CONCEPT3                                         =  5, // <concept> ::= <for_stmt>
        RULE_CONCEPT4                                         =  6, // <concept> ::= <switch_stmt>
        RULE_ID_ID                                            =  7, // <id> ::= Id
        RULE_DIGIT_DIGIT                                      =  8, // <digit> ::= Digit
        RULE_NEWLINE_NEWLINE                                  =  9, // <NewLine> ::= NewLine
        RULE_ASSIGN_EQ                                        = 10, // <assign> ::= <id> '=' <expr>
        RULE_EXPR_PLUS                                        = 11, // <expr> ::= <expr> '+' <term>
        RULE_EXPR_MINUS                                       = 12, // <expr> ::= <expr> '-' <term>
        RULE_EXPR                                             = 13, // <expr> ::= <term>
        RULE_TERM_TIMES                                       = 14, // <term> ::= <term> '*' <factor>
        RULE_TERM_DIV                                         = 15, // <term> ::= <term> '/' <factor>
        RULE_TERM_PERCENT                                     = 16, // <term> ::= <term> '%' <factor>
        RULE_TERM                                             = 17, // <term> ::= <factor>
        RULE_FACTOR_TIMESTIMES                                = 18, // <factor> ::= <factor> '**' <primary>
        RULE_FACTOR                                           = 19, // <factor> ::= <primary>
        RULE_PRIMARY_LPAREN_RPAREN                            = 20, // <primary> ::= '(' <expr> ')'
        RULE_PRIMARY                                          = 21, // <primary> ::= <id>
        RULE_PRIMARY2                                         = 22, // <primary> ::= <digit>
        RULE_IF_STMT_IF_LPAREN_RPAREN_COLON_START_END         = 23, // <if_stmt> ::= if '(' <cond> ')' ':' <NewLine> Start <stmt_list> End <e_cond>
        RULE_E_COND_EF_LPAREN_RPAREN_COLON_START_END          = 24, // <e_cond> ::= ef '(' <cond> ')' ':' <NewLine> Start <stmt_list> End <e_cond>
        RULE_E_COND_ELSE_COLON_START_END                      = 25, // <e_cond> ::= else ':' <NewLine> Start <stmt_list> End
        RULE_E_COND                                           = 26, // <e_cond> ::= 
        RULE_COND                                             = 27, // <cond> ::= <expr> <op> <expr>
        RULE_OP_LT                                            = 28, // <op> ::= '<'
        RULE_OP_GT                                            = 29, // <op> ::= '>'
        RULE_OP_EQEQ                                          = 30, // <op> ::= '=='
        RULE_OP_EXCLAMEQ                                      = 31, // <op> ::= '!='
        RULE_FOR_STMT_FOR_IN_COLON_START_END                  = 32, // <for_stmt> ::= for <id> in <range_expr> ':' <NewLine> Start <stmt_list> End
        RULE_RANGE_EXPR_RANGE_LPAREN_RPAREN                   = 33, // <range_expr> ::= range '(' <expr> ')'
        RULE_RANGE_EXPR_RANGE_LPAREN_COMMA_RPAREN             = 34, // <range_expr> ::= range '(' <expr> ',' <expr> ')'
        RULE_SWITCH_STMT_SWITCH_LPAREN_RPAREN_COLON_START_END = 35, // <switch_stmt> ::= switch '(' <expr> ')' ':' <NewLine> Start <case_list> End
        RULE_CASE_LIST                                        = 36, // <case_list> ::= <case> <case_list>
        RULE_CASE_LIST2                                       = 37, // <case_list> ::= <default_case>
        RULE_CASE_CASE_LPAREN_RPAREN_COLON                    = 38, // <case> ::= case '(' <id> ')' ':' <NewLine> <stmt_list>
        RULE_DEFAULT_CASE_DEFAULT_COLON                       = 39  // <default_case> ::= default ':' <NewLine> <stmt_list>
    };

    public class MyParser
    {
        private LALRParser parser;
        ListBox lst;
        ListBox ls;

        public MyParser(string filename,ListBox lst, ListBox ls)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            this.lst = lst;
            this.ls = ls;
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
            parser.OnTokenRead += new LALRParser.TokenReadHandler(TokenReadEvent);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PERCENT :
                //'%'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMESTIMES :
                //'**'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMA :
                //','
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLON :
                //':'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQEQ :
                //'=='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE :
                //case
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DEFAULT :
                //default
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT :
                //Digit
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EF :
                //ef
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_END :
                //End
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID :
                //Id
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IN :
                //in
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NEWLINE :
                //NewLine
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RANGE :
                //range
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_START :
                //Start
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH :
                //switch
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGN :
                //<assign>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE2 :
                //<case>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CASE_LIST :
                //<case_list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONCEPT :
                //<concept>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COND :
                //<cond>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DEFAULT_CASE :
                //<default_case>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT2 :
                //<digit>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_E_COND :
                //<e_cond>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPR :
                //<expr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FACTOR :
                //<factor>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR_STMT :
                //<for_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID2 :
                //<id>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF_STMT :
                //<if_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NEWLINE2 :
                //<NewLine>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OP :
                //<op>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PRIMARY :
                //<primary>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RANGE_EXPR :
                //<range_expr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STMT_LIST :
                //<stmt_list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SWITCH_STMT :
                //<switch_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TERM :
                //<term>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM_START_END :
                //<program> ::= Start <stmt_list> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LIST :
                //<stmt_list> ::= <concept>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LIST2 :
                //<stmt_list> ::= <concept> <stmt_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT :
                //<concept> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT2 :
                //<concept> ::= <if_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT3 :
                //<concept> ::= <for_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT4 :
                //<concept> ::= <switch_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ID_ID :
                //<id> ::= Id
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIGIT_DIGIT :
                //<digit> ::= Digit
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_NEWLINE_NEWLINE :
                //<NewLine> ::= NewLine
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGN_EQ :
                //<assign> ::= <id> '=' <expr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_PLUS :
                //<expr> ::= <expr> '+' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_MINUS :
                //<expr> ::= <expr> '-' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR :
                //<expr> ::= <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_TIMES :
                //<term> ::= <term> '*' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_DIV :
                //<term> ::= <term> '/' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_PERCENT :
                //<term> ::= <term> '%' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM :
                //<term> ::= <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_TIMESTIMES :
                //<factor> ::= <factor> '**' <primary>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR :
                //<factor> ::= <primary>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PRIMARY_LPAREN_RPAREN :
                //<primary> ::= '(' <expr> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PRIMARY :
                //<primary> ::= <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PRIMARY2 :
                //<primary> ::= <digit>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STMT_IF_LPAREN_RPAREN_COLON_START_END :
                //<if_stmt> ::= if '(' <cond> ')' ':' <NewLine> Start <stmt_list> End <e_cond>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_E_COND_EF_LPAREN_RPAREN_COLON_START_END :
                //<e_cond> ::= ef '(' <cond> ')' ':' <NewLine> Start <stmt_list> End <e_cond>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_E_COND_ELSE_COLON_START_END :
                //<e_cond> ::= else ':' <NewLine> Start <stmt_list> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_E_COND :
                //<e_cond> ::= 
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COND :
                //<cond> ::= <expr> <op> <expr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_LT :
                //<op> ::= '<'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_GT :
                //<op> ::= '>'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EQEQ :
                //<op> ::= '=='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EXCLAMEQ :
                //<op> ::= '!='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_STMT_FOR_IN_COLON_START_END :
                //<for_stmt> ::= for <id> in <range_expr> ':' <NewLine> Start <stmt_list> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_RANGE_EXPR_RANGE_LPAREN_RPAREN :
                //<range_expr> ::= range '(' <expr> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_RANGE_EXPR_RANGE_LPAREN_COMMA_RPAREN :
                //<range_expr> ::= range '(' <expr> ',' <expr> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_SWITCH_STMT_SWITCH_LPAREN_RPAREN_COLON_START_END :
                //<switch_stmt> ::= switch '(' <expr> ')' ':' <NewLine> Start <case_list> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_LIST :
                //<case_list> ::= <case> <case_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_LIST2 :
                //<case_list> ::= <default_case>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CASE_CASE_LPAREN_RPAREN_COLON :
                //<case> ::= case '(' <id> ')' ':' <NewLine> <stmt_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DEFAULT_CASE_DEFAULT_COLON :
                //<default_case> ::= default ':' <NewLine> <stmt_list>
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+" In Line: " + args.UnexpectedToken.Location.LineNr;
            lst.Items.Add(message);
            string m2 = "Excpected : " + args.ExpectedTokens.ToString().Replace(" ", " - ") + "";
            lst.Items.Add(m2);
            //todo: Report message to UI?
        }

        private void TokenReadEvent(LALRParser pr, TokenReadEventArgs args)
        {
            string info = args.Token.Text + " \t " + (SymbolConstants)args.Token.Symbol.Id; 
            ls.Items.Add(info);
        }

	}
}
