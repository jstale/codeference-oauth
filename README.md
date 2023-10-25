# codeference-oauth
## Setup
1. **Create an MVC Application: Create a new ASP.NET MVC application in Visual Studio or your preferred development environment.**
2. **Set Up OAuth2 in Google Cloud Console:**
   - Go to the [Google Cloud Console](https://console.cloud.google.com/).
   - Create a new project or select an existing one.
   - In the sidebar, navigate to "APIs & Services" > "Credentials."
   - Click "Create credentials" and select "OAuth client ID."
   - Choose "Web application" as the application type.
   - Add the authorized redirect URI, which should be something like http://**yourdomain.com**/callback. (Replace yourdomain.com with your actual domain or localhost for local development.)
   - After creating the OAuth client ID, you'll get a Client ID, Client Secret, Project Id. Save these values for your application.
3. Enable access to People API for your application by going to [https://console.developers.google.com/apis/api/people.googleapis.com/overview?project=**project-id**](https://console.developers.google.com/apis/api/people.googleapis.com/overview?project=project-id)

## Development 
Create necessary controllers and services to fetch an OAuth token for the user. User that token to authenticate when accessing Google'saj
