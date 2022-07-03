# Foundation.Data
Similar to entity framework, include json

## Gist
All access via datasource,

pass class & filters

in applications create 
publc class Names : DataEntity
{
    _TableName = "entity-names";
    [PrimaryKey]
    public int ID;
    public string Name;
}

this should transalte to a sql query or json via datasource
datasource is provided a connection string to either a SQL
db or json file 

provide
From = BaseTable
Join = From.[LeftRight]Join(Table) // Joiner .OnEq(Tbl1.Col, Tbl2.Col1).Add()

Where = From.Where(And(Eq(tbl1.col1, "str"), Eq(tbl2.col2, 3)))

ExecuteQuery(From)