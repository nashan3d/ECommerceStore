1. Define database connection string in appsettings json in EcommerceStore.API project
2. Run the update-database migration command to persist the product table in the database
    - Before run the migration command please follow below instructions...
	- ECommerceStore.API project should be Startup Project.
	- ECommerceStore.Infrastructure project shoulde be dafault project in the Package Manager Console
3. Start the ECommerceStore.API and ECommerceStore.IDP.Web(IdentityServer) at once 
4. Testing from Postman
	- Create a api request  in the postman.
	- In the Authorization Tab..
		- Select Type as OAuth 2.0
		- To Generate Access Token please follow instructions to be done in generate accesstoken window
		  - To Configure New Token
		    - Token Name : ECommerceAPITOken
			- GrantType  : Authenticaion Code (with PKCE)
			- Callback URL : keep as default
			- Tick the check box Authorize using browser
			- Auth URL : https://localhost:5443/connect/authorize
			- Access Token URL : https://localhost:5443/connect/token
			- ClientID : postman
			- Client Secret : secret
			- Code Challenge method : SHA-256
			- Scope : ECommerceStore.API role
			Note: Keep other fields as default
		- Click Generate Access Token in the bottom of the window
		- Prompt for Login Page
		- Use TestUsers credtials (Located in \ECommerceStore.IDP.Web\Quickstart\TestUsers.cs)
		-After login completed use generated access Token
   - For api urls please find from swagger https://localhost:7251/swagger/index.html
5.Test from console app.
  - After start the two projects API and ECommerceStore.IDP.Web
  - Right click on the ECommerceStore.Console.Client project
  - Select Debug -> Start Debug New Instance
  - It will launch console app and will return AccessToken and One Product Item in  Console window
			