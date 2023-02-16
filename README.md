<h1>Simple Files Management API using .Net Core</h1>

<h2>To Run:</h2>
<h4>
  <pre>docker compose up</pre>
  to set up the data base (MSSQL)
</h4>
<h4>Enjoy!</h4>

<h2>What could've been added?</h2>
<ul>
  <li>More granular tests</li>
  <li>Customised guards (such as on file extensions)</li>
  <li>Logging</li>
  <li>Custom docker compose for API</li>
</ul>

<h2>Technology stack</h2>
<ul>
  <li>.NetCore C#</li>
  <li>Docker</li>
  <li>MSSQL</li>
</ul>

<h2>Main Design and Functional Features</h2>
<ul>
  <li>Architecture Design: SOLID, DDD, Hexagonal</li>
  <li>CQRS (check GetUsersUseCase)</li>
  <li>Decoupling via MediatR</li>
  <li>Authentication: JWT, generated through ('api/uthentication/login')</li>
  <li>
    APIs
    <ul>
      <li>
        GET ('/api/v1/FilesManagement'): returns list of uploaded files withe
        their details
      </li>
      <li>
        POST('/api/v1/FilesManagement'): takes a file determins/persist its info
        and saves it
      </li>
      <li>
        DELETE('/api/v1/FilesManagement/id'): deletes a file and its data
        through the provided id
      </li>
      <li>
        GET('/api/v1/FilesManagement/id'): downloads a file through the provided
        id
      </li>
    </ul>
  </li>
  <li>Security: Authentication token, password hashing</li>
  <li>...</li>
</ul>

<h2>How it was developed</h2>
<p>
  The process tried to adhere as much as possible to: - SOLID (especially
  Seperation of concerns and IoC), - hexagonal architecture (dependencies are
  outwards) - DDD maintaining a clean domain layer where the business rules are
  inforced and an in the application layer used use cases instead of services -
  seperated the persistance logic from the business logic
</p>
The source code contains:
<ul>
  <li>
    2 modules (identity and FilesManagement.Core) each one of them containing 4
    seperate projects:
    <ul>
      <li>
        <b>Domain:</b> Contains the aggregate roots, domain entities and enforces
        the repositories' interfaces (IoC) although being persistance ignorant
        (due to the project's size not much was added to this project)
      </li>
      <li>
        <b>Application:</b> The orchstrator containing the various use cases that
        the module provides, it enforces its own interfaces (IoC) and is only
        dependant on the Domain Project (hexagonal)
      </li>
      <li>
        <b>Infra:</b> Where the not business-specific implementations are, it
        contains the data access logic (from ORMs to db contextss, migrations
        and repositories implementations) and various helpers/tools
      </li>
      <li><b>Tests:</b> A XUnit project to create automated tests</li>
    </ul>
  </li>
  <li>
    An API project where all the controllers are, this is the HTTP side of the
    project, it has no business logic and only communicates through MediatR.
    This project also boots up the solution.
  </li>
  <li>
    Common.Application and Common.Infra are projects that can contain common
    functionalities by layer
  </li>
</ul>
