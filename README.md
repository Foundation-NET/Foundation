# Foundation
 
## Components
- Config Reader (INI, KeyValPair) [Done]
- CsvReader [InProgress]
- Data (Generic unified data access - SQL, Json, XML) [TODO]
- Mail (Send Email) [TODO]

## Usage
Ensure that mail inherits ApplicationBase, then any class that 
uses the components should inherit ObjectBase this will expose:
    Resolve
    ResolveRequired
    CreateScope
    GetServiceScope
    GetRequiredServiceScope
These are used to resolve the components

## Create a component
Any standard component simply needs to implement an interface
and register itself in foundation. If using factory builder add
[Factory] attribute to ensure that ObjectBase runs the factory builder,
it will also pass any object to the builder, provided to the function.