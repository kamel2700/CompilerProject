%namespace CompilersProject.Compiler

%using CompilersProject.Compiler.AST
%using CompilersProject.Compiler.Lexer

%{
public ProgramRoot Root;
%}

%union {
    public string sval;
    public int ival;
    public double dval;
    public bool bval;
    public ProgramRoot prog;
    public SimpleDeclaration simpledec;
    public VariableDeclaration vardec;
    public TypeDeclaration typedec;
    public RoutineDeclaration routdec;
    public Parameters param;
    public ParameterDeclaration paramdec;
    public VarType type;
    public PrimitiveType primtype;
    public RecordType rectype;
    public VariableDeclarationRec vardecrec;
    public ArrayType arrtype;
    public Body body;
    public Statement stat;
    public Assignment assign;
    public RoutineCall routcall;
    public ExpressionRec exprec;
    public WhileLoop whileloop;
    public ForLoop forloop;
    public ForRange range;
    public IfStatement ifstat;
    public Expression exp;
    public RelationRec relrec;
    public Simple simple;
    public SummandRec sumrec;
    public Summand sum;
    public FactorRec factrec;
    public Factor fact;
    public Primary prim;
//    public IntegralLiteral intlit;
//    public RealLiteral reallit;
    public ModifiablePrimary modprim;
    public ModifiablePrimaryRec modprimrec;
//    public IdentifierRec identrec;
    public Relation rel;
}


%output = Yacc.cs




%token <sval> IDENTIFIER
%token <ival> NUMBER
%token <dval> REALNUM
%token <bval> BOOL

// Delimiters
%token LBRACE      //  {
%token RBRACE      //  }
%token OPEN_PAREN  //  (
%token CLOSE_PAREN //  )
%token LBRACKET    //  [
%token RBRACKET    //  ]
%token COMMA       //  ,
%token DOT         //  .
%token SEMICOLON   //  ;
%token COLON       //  :
%token RANGE       //  ..

// Operator signs
%token ASSIGN      //  :=
%token LESS        //  <
%token GREATER     //  >
%token EQUAL       //  =
%token LESSEQ      //  <=
%token GREATEREQ   //  >=
%token NOT_EQUAL   //  /=
%token PLUS        //  +
%token MINUS       //  -
%token MULT    //  *
%token DIV      //  /
%token MOD         //  %

%token VAR
%token IS
%token TYPE
%token XOR OR AND IN FOR WHILE REVERSE

%token INT REAL BOOLEAN
%token TRUE FALSE
%token RECORD ROUTINE
%token ARRAY IF THEN ELSE END
%token LOOP

%type <prog> ProgramRoot
%type <simpledec> SimpleDeclaration
%type <vardec> VariableDeclaration
%type <typedec> TypeDeclaration
%type <routdec> RoutineDeclaration
%type <param> Parameters
%type <paramdec> ParameterDeclaration
%type <type> VarType
%type <primtype> PrimitiveType
%type <rectype> RecordType
%type <vardecrec> VariableDeclarationRec
%type <arrtype> ArrayType
%type <body> Body
%type <stat> Statement
%type <assign> Assignment
%type <routcall> RoutineCall
%type <exprec> ExpressionRec
%type <whileloop> WhileLoop
%type <forloop> ForLoop
%type <range> ForRange
%type <ifstat> IfStatement
%type <exp> Expression
%type <relrec> RelationRec
%type <simple> Simple
%type <sumrec> SummandRec
%type <sum> Summand
%type <factrec> FactorRec
%type <fact> Factor
%type <prim> Primary
%type <intlit> IntegralLiteral
%type <reallit> RealLiteral
%type <modprim> ModifiablePrimary
%type <modprimrec> ModifiablePrimaryRec
// %type <identrec> IdentifierRec
%type <rel> Relation

%start ProgramRoot

%%

ProgramRoot
	: SimpleDeclaration ProgramRoot { $$ = new ProgramRoot($1, $2); Root = $$; }
	| RoutineDeclaration ProgramRoot {$$ = new ProgramRoot($1, $2); Root = $$; }
	| /* Empty */ { $$ = new ProgramRoot(); Root = $$; }
	;

SimpleDeclaration
	: VariableDeclaration SEMICOLON { $$ = new SimpleDeclaration($1); }
	| TypeDeclaration SEMICOLON { $$ = new SimpleDeclaration($1); }
	;

VariableDeclaration
	: VAR IDENTIFIER COLON VarType { $$ = new VariableDeclaration($2, $4); }
	| VAR IDENTIFIER COLON VarType IS Expression { $$ = new VariableDeclaration($2, $4, $6); }
	| VAR IDENTIFIER IS Expression { $$ = new VariableDeclaration($2, $4); }
	;

TypeDeclaration
	: TYPE IDENTIFIER IS VarType { $$ = new TypeDeclaration($2, $4); }
	;

RoutineDeclaration
	: ROUTINE IDENTIFIER OPEN_PAREN Parameters CLOSE_PAREN IS Body END SEMICOLON { $$ = new RoutineDeclaration($2, $4, $7); }
	| ROUTINE IDENTIFIER OPEN_PAREN Parameters CLOSE_PAREN COLON VarType IS Body END SEMICOLON { $$ = new RoutineDeclaration($2, $4, $7, $9); }
	;

Parameters
	: ParameterDeclaration { $$ = new Parameters($1); }
	| ParameterDeclaration COMMA Parameters { $$ = new Parameters($1, $3); }
	| /* Empty */ { $$ = new Parameters(); }
	;

ParameterDeclaration
	: IDENTIFIER COLON IDENTIFIER { $$ = new ParameterDeclaration($1, $3); }
	| IDENTIFIER COLON PrimitiveType { $$ = new ParameterDeclaration($1, $3); }
	;

VarType 
	: PrimitiveType { $$ = new VarType($1); }
	| ArrayType { $$ = new VarType($1);}
	| RecordType { $$ = new VarType($1);}
	| IDENTIFIER { $$ = new VarType($1);}
	;

PrimitiveType
	: INT { $$ = PrimitiveType.Int;}
	| REAL { $$ = PrimitiveType.Real;}
	| BOOLEAN { $$ = PrimitiveType.Boolean; }
	;

RecordType
	: RECORD VariableDeclarationRec END { $$ = new RecordType($2); }
	;

VariableDeclarationRec
	: VariableDeclaration SEMICOLON { $$ = new VariableDeclarationRec($1); }
	| VariableDeclaration SEMICOLON VariableDeclarationRec { $$ = new VariableDeclarationRec($1, $3); }
	;

ArrayType
	: ARRAY LBRACKET Expression RBRACKET VarType { $$ = new ArrayType($3, $5); }
	;

Body 	
	: SimpleDeclaration Body { $$ = new Body($1, $2);}
	| Statement Body { $$ = new Body($1, $2);}
	| /* Empty */ { $$ = new Body();}
	;

Statement
	: Assignment SEMICOLON { $$ = new Statement($1); }
	| RoutineCall SEMICOLON { $$ = new Statement($1); }
	| WhileLoop SEMICOLON { $$ = new Statement($1); }
	| ForLoop SEMICOLON { $$ = new Statement($1); }
	| IfStatement SEMICOLON { $$ = new Statement($1); }
	;

Assignment
	: ModifiablePrimary ASSIGN Expression { $$ = new Assignment($1, $3); }
	;

RoutineCall
	: IDENTIFIER OPEN_PAREN CLOSE_PAREN { $$ = new RoutineCall($1); }
	| IDENTIFIER OPEN_PAREN Expression CLOSE_PAREN { $$ = new RoutineCall($1, $3); }
	| IDENTIFIER OPEN_PAREN Expression COMMA ExpressionRec CLOSE_PAREN { $$ = new RoutineCall($1, $3, $5); }
	;

Relation
    : Simple { $$ = new Relation($1); }
    | Simple LESS Simple { $$ = new Relation($1, Operation.Lt ,$3); }
    | Simple GREATER Simple { $$ = new Relation($1, Operation.Gt, $3); }
    | Simple GREATEREQ Simple { $$ = new Relation($1, Operation.Ge, $3); }
    | Simple EQUAL Simple { $$ = new Relation($1, Operation.Eq, $3); }
    | Simple NOT_EQUAL Simple { $$ = new Relation($1, Operation.Ne, $3); }
    | Simple LESSEQ Simple { $$ = new Relation($1, Operation.Le, $3); }
    ;

ExpressionRec
	: Expression { $$ = new ExpressionRec($1); }
	| Expression COMMA ExpressionRec { $$ = new ExpressionRec($1, $3); }
	;

WhileLoop
	: WHILE Expression LOOP Body END { $$ = new WhileLoop($2, $4); }
	;

ForLoop
	: FOR IDENTIFIER ForRange LOOP Body END { $$ = new ForLoop($2, $3, $5); }
	;

ForRange
	: IN Expression RANGE Expression { $$ = new ForRange($2, $4); }
	| IN REVERSE Expression RANGE Expression  { $$ = new ForRange($3, $5 , true); }
	;

IfStatement
	: IF Expression THEN Body END { $$ = new IfStatement($2, $4); }
	| IF Expression THEN Body ELSE Body END { $$ = new IfStatement($2, $4, $6); }
	;

Expression
	: Relation { $$ = new Expression($1); }
	| Relation RelationRec { $$ = new Expression($1, $2); }
	;

RelationRec
	: AND Relation { $$ = new RelationRec(Operation.And, $2); }
	| OR Relation { $$ = new RelationRec(Operation.Or, $2); }
	| XOR Relation { $$ = new RelationRec(Operation.Xor, $2); }
	| AND Relation RelationRec { $$ = new RelationRec(Operation.And, $2, $3); }
	| OR Relation RelationRec { $$ = new RelationRec(Operation.Or, $2, $3); }
	| XOR Relation RelationRec { $$ = new RelationRec(Operation.Xor, $2, $3); }
	;

Simple 
	: Summand { $$ = new Simple($1); }
	| Summand SummandRec { $$ = new Simple($1, $2); }
	;

SummandRec 
	: PLUS Summand { $$ = new SummandRec(Operation.Add, $2); }
	| MINUS Summand { $$ = new SummandRec(Operation.Sub, $2); }
	| PLUS Summand SummandRec { $$ = new SummandRec(Operation.Add, $2, $3); }
	| MINUS Summand SummandRec { $$ = new SummandRec(Operation.Add, $2, $3); }
	;

Summand
	: Factor { $$ = new Summand($1); }
	| Factor FactorRec { $$ = new Summand($1, $2); }
	;

FactorRec
	: MULT Factor { $$ = new FactorRec(Operation.Mul, $2); }
	| DIV Factor { $$ = new FactorRec(Operation.Div, $2); }
	| MOD Factor { $$ = new FactorRec(Operation.Mod, $2); }
	| MULT Factor FactorRec { $$ = new FactorRec(Operation.Mul, $2, $3); }
	| DIV Factor FactorRec { $$ = new FactorRec(Operation.Div, $2, $3); }
	| MOD Factor FactorRec { $$ = new FactorRec(Operation.Mod, $2, $3); }
	;

Factor 
	: Primary { $$ = new Factor($1); }
	| OPEN_PAREN Expression CLOSE_PAREN { $$ = new Factor($2); }
	;

Primary 
	: NUMBER { $$ = new Primary($1); }
	| REALNUM { $$ = new Primary($1); }
	| TRUE { $$ = new Primary(true); }
	| FALSE { $$ = new Primary(false); }
	| ModifiablePrimary { $$ = new Primary($1); }
	;

IntegralLiteral
    : PLUS NUMBER
    | MINUS NUMBER
    | NUMBER
    ;

RealLiteral
    : REALNUM
    | MINUS REALNUM
    | PLUS REALNUM
    ;

ModifiablePrimary
	: IDENTIFIER { $$ = new ModifiablePrimary($1); }
	| IDENTIFIER ModifiablePrimaryRec { $$ = new ModifiablePrimary($1, $2); }
	;

ModifiablePrimaryRec
	: DOT IDENTIFIER { $$ = new ModifiablePrimaryRec($2); }
	| LBRACKET Expression RBRACKET { $$ = new ModifiablePrimaryRec($2); }
	| DOT IDENTIFIER ModifiablePrimaryRec { $$ = new ModifiablePrimaryRec($2, $3); }
	| LBRACKET Expression RBRACKET ModifiablePrimaryRec { $$ = new ModifiablePrimaryRec($2, $4); }
	;
