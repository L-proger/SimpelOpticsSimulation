using System.IO;
/*
public class MyParser
{
    private GOLD.Parser parser = new GOLD.Parser(); 

    private enum SymbolIndex
    {
        @Eof = 0,                                  // (EOF)
        @Error = 1,                                // (Error)
        @Whitespace = 2,                           // Whitespace
        @Minus = 3,                                // '-'
        @Minusminus = 4,                           // '--'
        @Exclam = 5,                               // '!'
        @Exclameq = 6,                             // '!='
        @Quote = 7,                                // '"'
        @Percent = 8,                              // '%'
        @Percenteq = 9,                            // '%='
        @Amp = 10,                                 // '&'
        @Ampamp = 11,                              // '&&'
        @Ampeq = 12,                               // '&='
        @Lparen = 13,                              // '('
        @Rparen = 14,                              // ')'
        @Times = 15,                               // '*'
        @Timeseq = 16,                             // '*='
        @Comma = 17,                               // ','
        @Div = 18,                                 // '/'
        @Diveq = 19,                               // '/='
        @Colon = 20,                               // ':'
        @Question = 21,                            // '?'
        @Backslash = 22,                           // '\'
        @Caret = 23,                               // '^'
        @Careteq = 24,                             // '^='
        @Pipe = 25,                                // '|'
        @Pipepipe = 26,                            // '||'
        @Pipeeq = 27,                              // '|='
        @Tilde = 28,                               // '~'
        @Plus = 29,                                // '+'
        @Plusplus = 30,                            // '++'
        @Pluseq = 31,                              // '+='
        @Lt = 32,                                  // '<'
        @Ltlt = 33,                                // '<<'
        @Ltlteq = 34,                              // '<<='
        @Lteq = 35,                                // '<='
        @Eq = 36,                                  // '='
        @Minuseq = 37,                             // '-='
        @Eqeq = 38,                                // '=='
        @Gt = 39,                                  // '>'
        @Minusgt = 40,                             // '->'
        @Gteq = 41,                                // '>='
        @Gtgt = 42,                                // '>>'
        @Gtgteq = 43,                              // '>>='
        @A = 44,                                   // A
        @Charliteral = 45,                         // CharLiteral
        @Decliteral = 46,                          // DecLiteral
        @F = 47,                                   // F
        @False = 48,                               // false
        @Hexliteral = 49,                          // HexLiteral
        @Identifier = 50,                          // Identifier
        @Realliteral = 51,                         // RealLiteral
        @Stringliteral = 52,                       // StringLiteral
        @True = 53,                                // true
        @Addexp = 54,                              // <Add Exp>
        @Andexp = 55,                              // <And Exp>
        @Args = 56,                                // <Args>
        @Assignop = 57,                            // <AssignOp>
        @Compareexp = 58,                          // <Compare Exp>
        @Conditionalexp = 59,                      // <Conditional Exp>
        @Equalityexp = 60,                         // <Equality Exp>
        @Expression = 61,                          // <Expression>
        @Expressionlist = 62,                      // <Expression List>
        @Formalparams = 63,                        // <FormalParams>
        @Formalparamslist = 64,                    // <FormalParamsList>
        @Literal = 65,                             // <Literal>
        @Logicalandexp = 66,                       // <Logical And Exp>
        @Logicalorexp = 67,                        // <Logical Or Exp>
        @Logicalxorexp = 68,                       // <Logical Xor Exp>
        @Match = 69,                               // <Match>
        @Matchleftcontext = 70,                    // <MatchLeftContext>
        @Matchprime = 71,                          // <MatchPrime>
        @Matchrightcontext = 72,                   // <MatchRightContext>
        @Multexp = 73,                             // <Mult Exp>
        @Opname = 74,                              // <OpName>
        @Opsleft = 75,                             // <OpsLeft>
        @Opsleftlist = 76,                         // <OpsLeftList>
        @Opsright = 77,                            // <OpsRight>
        @Opsrightlist = 78,                        // <OpsRightList>
        @Orexp = 79,                               // <Or Exp>
        @Primary = 80,                             // <Primary>
        @Primaryexp = 81,                          // <Primary Exp>
        @Replace = 82,                             // <Replace>
        @Rule = 83,                                // <Rule>
        @Shiftexp = 84,                            // <Shift Exp>
        @Unaryexp = 85                             // <Unary Exp>
    }

    private enum ProductionIndex
    {
        @Assignop_Eq = 0,                          // <AssignOp> ::= '='
        @Assignop_Minusgt = 1,                     // <AssignOp> ::= '->'
        @Literal_True = 2,                         // <Literal> ::= true
        @Literal_False = 3,                        // <Literal> ::= false
        @Literal_Decliteral = 4,                   // <Literal> ::= DecLiteral
        @Literal_Hexliteral = 5,                   // <Literal> ::= HexLiteral
        @Literal_Realliteral = 6,                  // <Literal> ::= RealLiteral
        @Literal_Charliteral = 7,                  // <Literal> ::= CharLiteral
        @Literal_Stringliteral = 8,                // <Literal> ::= StringLiteral
        @Opname_Plus = 9,                          // <OpName> ::= '+'
        @Opname_Minus = 10,                        // <OpName> ::= '-'
        @Opname_Amp = 11,                          // <OpName> ::= '&'
        @Opname_Caret = 12,                        // <OpName> ::= '^'
        @Opname_Backslash = 13,                    // <OpName> ::= '\'
        @Opname_Div = 14,                          // <OpName> ::= '/'
        @Opname_Pipe = 15,                         // <OpName> ::= '|'
        @Opname_Exclam = 16,                       // <OpName> ::= '!'
        @Opname_Quote = 17,                        // <OpName> ::= '"'
        @Opname_A = 18,                            // <OpName> ::= A
        @Opname_F = 19,                            // <OpName> ::= F
        @Opsrightlist = 20,                        // <OpsRightList> ::= <OpsRight>
        @Opsrightlist2 = 21,                       // <OpsRightList> ::= <OpsRightList> <OpsRight>
        @Opsright = 22,                            // <OpsRight> ::= <OpName>
        @Opsright2 = 23,                           // <OpsRight> ::= <OpName> <Args>
        @Expressionlist = 24,                      // <Expression List> ::= <Expression>
        @Expressionlist_Comma = 25,                // <Expression List> ::= <Expression> ',' <Expression List>
        @Expression_Eq = 26,                       // <Expression> ::= <Conditional Exp> '=' <Expression>
        @Expression_Pluseq = 27,                   // <Expression> ::= <Conditional Exp> '+=' <Expression>
        @Expression_Minuseq = 28,                  // <Expression> ::= <Conditional Exp> '-=' <Expression>
        @Expression_Timeseq = 29,                  // <Expression> ::= <Conditional Exp> '*=' <Expression>
        @Expression_Diveq = 30,                    // <Expression> ::= <Conditional Exp> '/=' <Expression>
        @Expression_Careteq = 31,                  // <Expression> ::= <Conditional Exp> '^=' <Expression>
        @Expression_Ampeq = 32,                    // <Expression> ::= <Conditional Exp> '&=' <Expression>
        @Expression_Pipeeq = 33,                   // <Expression> ::= <Conditional Exp> '|=' <Expression>
        @Expression_Percenteq = 34,                // <Expression> ::= <Conditional Exp> '%=' <Expression>
        @Expression_Ltlteq = 35,                   // <Expression> ::= <Conditional Exp> '<<=' <Expression>
        @Expression_Gtgteq = 36,                   // <Expression> ::= <Conditional Exp> '>>=' <Expression>
        @Expression = 37,                          // <Expression> ::= <Conditional Exp>
        @Conditionalexp_Question_Colon = 38,       // <Conditional Exp> ::= <Or Exp> '?' <Or Exp> ':' <Conditional Exp>
        @Conditionalexp = 39,                      // <Conditional Exp> ::= <Or Exp>
        @Orexp_Pipepipe = 40,                      // <Or Exp> ::= <Or Exp> '||' <And Exp>
        @Orexp = 41,                               // <Or Exp> ::= <And Exp>
        @Andexp_Ampamp = 42,                       // <And Exp> ::= <And Exp> '&&' <Logical Or Exp>
        @Andexp = 43,                              // <And Exp> ::= <Logical Or Exp>
        @Logicalorexp_Pipe = 44,                   // <Logical Or Exp> ::= <Logical Or Exp> '|' <Logical Xor Exp>
        @Logicalorexp = 45,                        // <Logical Or Exp> ::= <Logical Xor Exp>
        @Logicalxorexp_Caret = 46,                 // <Logical Xor Exp> ::= <Logical Xor Exp> '^' <Logical And Exp>
        @Logicalxorexp = 47,                       // <Logical Xor Exp> ::= <Logical And Exp>
        @Logicalandexp_Amp = 48,                   // <Logical And Exp> ::= <Logical And Exp> '&' <Equality Exp>
        @Logicalandexp = 49,                       // <Logical And Exp> ::= <Equality Exp>
        @Equalityexp_Eqeq = 50,                    // <Equality Exp> ::= <Equality Exp> '==' <Compare Exp>
        @Equalityexp_Exclameq = 51,                // <Equality Exp> ::= <Equality Exp> '!=' <Compare Exp>
        @Equalityexp = 52,                         // <Equality Exp> ::= <Compare Exp>
        @Compareexp_Lt = 53,                       // <Compare Exp> ::= <Compare Exp> '<' <Shift Exp>
        @Compareexp_Gt = 54,                       // <Compare Exp> ::= <Compare Exp> '>' <Shift Exp>
        @Compareexp_Lteq = 55,                     // <Compare Exp> ::= <Compare Exp> '<=' <Shift Exp>
        @Compareexp_Gteq = 56,                     // <Compare Exp> ::= <Compare Exp> '>=' <Shift Exp>
        @Compareexp = 57,                          // <Compare Exp> ::= <Shift Exp>
        @Shiftexp_Ltlt = 58,                       // <Shift Exp> ::= <Shift Exp> '<<' <Add Exp>
        @Shiftexp_Gtgt = 59,                       // <Shift Exp> ::= <Shift Exp> '>>' <Add Exp>
        @Shiftexp = 60,                            // <Shift Exp> ::= <Add Exp>
        @Addexp_Plus = 61,                         // <Add Exp> ::= <Add Exp> '+' <Mult Exp>
        @Addexp_Minus = 62,                        // <Add Exp> ::= <Add Exp> '-' <Mult Exp>
        @Addexp = 63,                              // <Add Exp> ::= <Mult Exp>
        @Multexp_Times = 64,                       // <Mult Exp> ::= <Mult Exp> '*' <Unary Exp>
        @Multexp_Div = 65,                         // <Mult Exp> ::= <Mult Exp> '/' <Unary Exp>
        @Multexp_Percent = 66,                     // <Mult Exp> ::= <Mult Exp> '%' <Unary Exp>
        @Multexp = 67,                             // <Mult Exp> ::= <Unary Exp>
        @Unaryexp_Exclam = 68,                     // <Unary Exp> ::= '!' <Unary Exp>
        @Unaryexp_Tilde = 69,                      // <Unary Exp> ::= '~' <Unary Exp>
        @Unaryexp_Minus = 70,                      // <Unary Exp> ::= '-' <Unary Exp>
        @Unaryexp_Plusplus = 71,                   // <Unary Exp> ::= '++' <Unary Exp>
        @Unaryexp_Minusminus = 72,                 // <Unary Exp> ::= '--' <Unary Exp>
        @Unaryexp = 73,                            // <Unary Exp> ::= <Primary Exp>
        @Primaryexp = 74,                          // <Primary Exp> ::= <Primary>
        @Primaryexp_Lparen_Rparen = 75,            // <Primary Exp> ::= '(' <Expression> ')'
        @Primary = 76,                             // <Primary> ::= <Literal>
        @Primary_Identifier = 77,                  // <Primary> ::= Identifier
        @Args_Lparen_Rparen = 78,                  // <Args> ::= '(' <Expression List> ')'
        @Formalparamslist_Comma_Identifier = 79,   // <FormalParamsList> ::= <FormalParamsList> ',' Identifier
        @Formalparamslist_Identifier = 80,         // <FormalParamsList> ::= Identifier
        @Formalparams_Lparen_Rparen = 81,          // <FormalParams> ::= '(' <FormalParamsList> ')'
        @Opsleft = 82,                             // <OpsLeft> ::= <OpName>
        @Opsleft2 = 83,                            // <OpsLeft> ::= <OpName> <FormalParams>
        @Opsleftlist = 84,                         // <OpsLeftList> ::= <OpsLeft>
        @Opsleftlist2 = 85,                        // <OpsLeftList> ::= <OpsLeftList> <OpsLeft>
        @Matchleftcontext_Lt = 86,                 // <MatchLeftContext> ::= <OpsLeftList> '<'
        @Matchprime = 87,                          // <MatchPrime> ::= <OpsLeftList>
        @Matchrightcontext_Gt = 88,                // <MatchRightContext> ::= '>' <OpsLeftList>
        @Match = 89,                               // <Match> ::= <MatchPrime>
        @Match2 = 90,                              // <Match> ::= <MatchLeftContext> <MatchPrime>
        @Match3 = 91,                              // <Match> ::= <MatchPrime> <MatchRightContext>
        @Match4 = 92,                              // <Match> ::= <MatchLeftContext> <MatchPrime> <MatchRightContext>
        @Replace = 93,                             // <Replace> ::= <OpsRightList>
        @Rule = 94                                 // <Rule> ::= <Match> <AssignOp> <Replace>
    }

    public object program;     //You might derive a specific object

    public void Setup()
    {
        //This procedure can be called to load the parse tables. The class can
        //read tables using a BinaryReader.

        parser.LoadTables(@"C:\Users\l-pro\Desktop\lsystem.egt");
       // parser.LoadTables(Path.Combine(Application.StartupPath, "grammar.egt"));
    }
    
    public bool Parse(TextReader reader)
    {
        //This procedure starts the GOLD Parser Engine and handles each of the
        //messages it returns. Each time a reduction is made, you can create new
        //custom object and reassign the .CurrentReduction property. Otherwise, 
        //the system will use the Reduction object that was returned.
        //
        //The resulting tree will be a pure representation of the language 
        //and will be ready to implement.

        GOLD.ParseMessage response; 
        bool done;                      //Controls when we leave the loop
        bool accepted = false;          //Was the parse successful?

        parser.Open(reader);
        parser.TrimReductions = false;  //Please read about this feature before enabling  

        done = false;
        while (!done)
        {
            response = parser.Parse();

            switch (response)
            {
                case GOLD.ParseMessage.LexicalError:
                    //Cannot recognize token
                    done = true;
                    break;

                case GOLD.ParseMessage.SyntaxError:
                    //Expecting a different token
                    done = true;
                    break;

                case GOLD.ParseMessage.Reduction:
                    //Create a customized object to store the reduction

                    parser.CurrentReduction = CreateNewObject(parser.CurrentReduction as GOLD.Reduction);
                    break;

                case GOLD.ParseMessage.Accept:
                    //Accepted!
                    //program = parser.CurrentReduction   //The root node!                 
                    done = true;
                    accepted = true;
                    break;

                case GOLD.ParseMessage.TokenRead:
                    //You don't have to do anything here.
                    break;

                case GOLD.ParseMessage.InternalError:
                    //INTERNAL ERROR! Something is horribly wrong.
                    done = true;
                    break;

                case GOLD.ParseMessage.NotLoadedError:
                    //This error occurs if the CGT was not loaded.                   
                    done = true;
                    break;

                case GOLD.ParseMessage.GroupError: 
                    //GROUP ERROR! Unexpected end of file
                    done = true;
                    break;
            } 
        } //while

        return accepted;
    }
    
    private object CreateNewObject(GOLD.Reduction r)
    { 
        object result = null;
        
        switch( (ProductionIndex) r.Parent.TableIndex())
        {
            case ProductionIndex.Assignop_Eq:                 
                // <AssignOp> ::= '='
                break;

            case ProductionIndex.Assignop_Minusgt:                 
                // <AssignOp> ::= '->'
                break;

            case ProductionIndex.Literal_True:                 
                // <Literal> ::= true
                break;

            case ProductionIndex.Literal_False:                 
                // <Literal> ::= false
                break;

            case ProductionIndex.Literal_Decliteral:                 
                // <Literal> ::= DecLiteral
                break;

            case ProductionIndex.Literal_Hexliteral:                 
                // <Literal> ::= HexLiteral
                break;

            case ProductionIndex.Literal_Realliteral:                 
                // <Literal> ::= RealLiteral
                break;

            case ProductionIndex.Literal_Charliteral:                 
                // <Literal> ::= CharLiteral
                break;

            case ProductionIndex.Literal_Stringliteral:                 
                // <Literal> ::= StringLiteral
                break;

            case ProductionIndex.Opname_Plus:                 
                // <OpName> ::= '+'
                break;

            case ProductionIndex.Opname_Minus:                 
                // <OpName> ::= '-'
                break;

            case ProductionIndex.Opname_Amp:                 
                // <OpName> ::= '&'
                break;

            case ProductionIndex.Opname_Caret:                 
                // <OpName> ::= '^'
                break;

            case ProductionIndex.Opname_Backslash:                 
                // <OpName> ::= '\'
                break;

            case ProductionIndex.Opname_Div:                 
                // <OpName> ::= '/'
                break;

            case ProductionIndex.Opname_Pipe:                 
                // <OpName> ::= '|'
                break;

            case ProductionIndex.Opname_Exclam:                 
                // <OpName> ::= '!'
                break;

            case ProductionIndex.Opname_Quote:                 
                // <OpName> ::= '"'
                break;

            case ProductionIndex.Opname_A:                 
                // <OpName> ::= A
                break;

            case ProductionIndex.Opname_F:                 
                // <OpName> ::= F
                break;

            case ProductionIndex.Opsrightlist:                 
                // <OpsRightList> ::= <OpsRight>
                break;

            case ProductionIndex.Opsrightlist2:                 
                // <OpsRightList> ::= <OpsRightList> <OpsRight>
                break;

            case ProductionIndex.Opsright:                 
                // <OpsRight> ::= <OpName>
                break;

            case ProductionIndex.Opsright2:                 
                // <OpsRight> ::= <OpName> <Args>
                break;

            case ProductionIndex.Expressionlist:                 
                // <Expression List> ::= <Expression>
                break;

            case ProductionIndex.Expressionlist_Comma:                 
                // <Expression List> ::= <Expression> ',' <Expression List>
                break;

            case ProductionIndex.Expression_Eq:                 
                // <Expression> ::= <Conditional Exp> '=' <Expression>
                break;

            case ProductionIndex.Expression_Pluseq:                 
                // <Expression> ::= <Conditional Exp> '+=' <Expression>
                break;

            case ProductionIndex.Expression_Minuseq:                 
                // <Expression> ::= <Conditional Exp> '-=' <Expression>
                break;

            case ProductionIndex.Expression_Timeseq:                 
                // <Expression> ::= <Conditional Exp> '*=' <Expression>
                break;

            case ProductionIndex.Expression_Diveq:                 
                // <Expression> ::= <Conditional Exp> '/=' <Expression>
                break;

            case ProductionIndex.Expression_Careteq:                 
                // <Expression> ::= <Conditional Exp> '^=' <Expression>
                break;

            case ProductionIndex.Expression_Ampeq:                 
                // <Expression> ::= <Conditional Exp> '&=' <Expression>
                break;

            case ProductionIndex.Expression_Pipeeq:                 
                // <Expression> ::= <Conditional Exp> '|=' <Expression>
                break;

            case ProductionIndex.Expression_Percenteq:                 
                // <Expression> ::= <Conditional Exp> '%=' <Expression>
                break;

            case ProductionIndex.Expression_Ltlteq:                 
                // <Expression> ::= <Conditional Exp> '<<=' <Expression>
                break;

            case ProductionIndex.Expression_Gtgteq:                 
                // <Expression> ::= <Conditional Exp> '>>=' <Expression>
                break;

            case ProductionIndex.Expression:                 
                // <Expression> ::= <Conditional Exp>
                break;

            case ProductionIndex.Conditionalexp_Question_Colon:                 
                // <Conditional Exp> ::= <Or Exp> '?' <Or Exp> ':' <Conditional Exp>
                break;

            case ProductionIndex.Conditionalexp:                 
                // <Conditional Exp> ::= <Or Exp>
                break;

            case ProductionIndex.Orexp_Pipepipe:                 
                // <Or Exp> ::= <Or Exp> '||' <And Exp>
                break;

            case ProductionIndex.Orexp:                 
                // <Or Exp> ::= <And Exp>
                break;

            case ProductionIndex.Andexp_Ampamp:                 
                // <And Exp> ::= <And Exp> '&&' <Logical Or Exp>
                break;

            case ProductionIndex.Andexp:                 
                // <And Exp> ::= <Logical Or Exp>
                break;

            case ProductionIndex.Logicalorexp_Pipe:                 
                // <Logical Or Exp> ::= <Logical Or Exp> '|' <Logical Xor Exp>
                break;

            case ProductionIndex.Logicalorexp:                 
                // <Logical Or Exp> ::= <Logical Xor Exp>
                break;

            case ProductionIndex.Logicalxorexp_Caret:                 
                // <Logical Xor Exp> ::= <Logical Xor Exp> '^' <Logical And Exp>
                break;

            case ProductionIndex.Logicalxorexp:                 
                // <Logical Xor Exp> ::= <Logical And Exp>
                break;

            case ProductionIndex.Logicalandexp_Amp:                 
                // <Logical And Exp> ::= <Logical And Exp> '&' <Equality Exp>
                break;

            case ProductionIndex.Logicalandexp:                 
                // <Logical And Exp> ::= <Equality Exp>
                break;

            case ProductionIndex.Equalityexp_Eqeq:                 
                // <Equality Exp> ::= <Equality Exp> '==' <Compare Exp>
                break;

            case ProductionIndex.Equalityexp_Exclameq:                 
                // <Equality Exp> ::= <Equality Exp> '!=' <Compare Exp>
                break;

            case ProductionIndex.Equalityexp:                 
                // <Equality Exp> ::= <Compare Exp>
                break;

            case ProductionIndex.Compareexp_Lt:                 
                // <Compare Exp> ::= <Compare Exp> '<' <Shift Exp>
                break;

            case ProductionIndex.Compareexp_Gt:                 
                // <Compare Exp> ::= <Compare Exp> '>' <Shift Exp>
                break;

            case ProductionIndex.Compareexp_Lteq:                 
                // <Compare Exp> ::= <Compare Exp> '<=' <Shift Exp>
                break;

            case ProductionIndex.Compareexp_Gteq:                 
                // <Compare Exp> ::= <Compare Exp> '>=' <Shift Exp>
                break;

            case ProductionIndex.Compareexp:                 
                // <Compare Exp> ::= <Shift Exp>
                break;

            case ProductionIndex.Shiftexp_Ltlt:                 
                // <Shift Exp> ::= <Shift Exp> '<<' <Add Exp>
                break;

            case ProductionIndex.Shiftexp_Gtgt:                 
                // <Shift Exp> ::= <Shift Exp> '>>' <Add Exp>
                break;

            case ProductionIndex.Shiftexp:                 
                // <Shift Exp> ::= <Add Exp>
                break;

            case ProductionIndex.Addexp_Plus:                 
                // <Add Exp> ::= <Add Exp> '+' <Mult Exp>
                break;

            case ProductionIndex.Addexp_Minus:                 
                // <Add Exp> ::= <Add Exp> '-' <Mult Exp>
                break;

            case ProductionIndex.Addexp:                 
                // <Add Exp> ::= <Mult Exp>
                break;

            case ProductionIndex.Multexp_Times:                 
                // <Mult Exp> ::= <Mult Exp> '*' <Unary Exp>
                break;

            case ProductionIndex.Multexp_Div:                 
                // <Mult Exp> ::= <Mult Exp> '/' <Unary Exp>
                break;

            case ProductionIndex.Multexp_Percent:                 
                // <Mult Exp> ::= <Mult Exp> '%' <Unary Exp>
                break;

            case ProductionIndex.Multexp:                 
                // <Mult Exp> ::= <Unary Exp>
                break;

            case ProductionIndex.Unaryexp_Exclam:                 
                // <Unary Exp> ::= '!' <Unary Exp>
                break;

            case ProductionIndex.Unaryexp_Tilde:                 
                // <Unary Exp> ::= '~' <Unary Exp>
                break;

            case ProductionIndex.Unaryexp_Minus:                 
                // <Unary Exp> ::= '-' <Unary Exp>
                break;

            case ProductionIndex.Unaryexp_Plusplus:                 
                // <Unary Exp> ::= '++' <Unary Exp>
                break;

            case ProductionIndex.Unaryexp_Minusminus:                 
                // <Unary Exp> ::= '--' <Unary Exp>
                break;

            case ProductionIndex.Unaryexp:                 
                // <Unary Exp> ::= <Primary Exp>
                break;

            case ProductionIndex.Primaryexp:                 
                // <Primary Exp> ::= <Primary>
                break;

            case ProductionIndex.Primaryexp_Lparen_Rparen:                 
                // <Primary Exp> ::= '(' <Expression> ')'
                break;

            case ProductionIndex.Primary:                 
                // <Primary> ::= <Literal>
                break;

            case ProductionIndex.Primary_Identifier:                 
                // <Primary> ::= Identifier
                break;

            case ProductionIndex.Args_Lparen_Rparen:                 
                // <Args> ::= '(' <Expression List> ')'
                break;

            case ProductionIndex.Formalparamslist_Comma_Identifier:                 
                // <FormalParamsList> ::= <FormalParamsList> ',' Identifier
                break;

            case ProductionIndex.Formalparamslist_Identifier:                 
                // <FormalParamsList> ::= Identifier
                break;

            case ProductionIndex.Formalparams_Lparen_Rparen:                 
                // <FormalParams> ::= '(' <FormalParamsList> ')'
                break;

            case ProductionIndex.Opsleft:                 
                // <OpsLeft> ::= <OpName>
                break;

            case ProductionIndex.Opsleft2:                 
                // <OpsLeft> ::= <OpName> <FormalParams>
                break;

            case ProductionIndex.Opsleftlist:                 
                // <OpsLeftList> ::= <OpsLeft>
                break;

            case ProductionIndex.Opsleftlist2:                 
                // <OpsLeftList> ::= <OpsLeftList> <OpsLeft>
                break;

            case ProductionIndex.Matchleftcontext_Lt:                 
                // <MatchLeftContext> ::= <OpsLeftList> '<'
                break;

            case ProductionIndex.Matchprime:                 
                // <MatchPrime> ::= <OpsLeftList>
                break;

            case ProductionIndex.Matchrightcontext_Gt:                 
                // <MatchRightContext> ::= '>' <OpsLeftList>
                break;

            case ProductionIndex.Match:                 
                // <Match> ::= <MatchPrime>
                break;

            case ProductionIndex.Match2:                 
                // <Match> ::= <MatchLeftContext> <MatchPrime>
                break;

            case ProductionIndex.Match3:                 
                // <Match> ::= <MatchPrime> <MatchRightContext>
                break;

            case ProductionIndex.Match4:                 
                // <Match> ::= <MatchLeftContext> <MatchPrime> <MatchRightContext>
                break;

            case ProductionIndex.Replace:                 
                // <Replace> ::= <OpsRightList>
                break;

            case ProductionIndex.Rule:                 
                // <Rule> ::= <Match> <AssignOp> <Replace>
                break;

        }  //switch

        return result;
    }
    
}; //MyParser
*/