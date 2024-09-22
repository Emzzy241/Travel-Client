// The ApiHelper class, a class that contains the definition for our ApiHelper.GetAll() method which actually handles making a call tou our Travel Api

using RestSharp;
using System;
using System.Threading.Tasks;
using TravelClient.Models;


namespace TravelClient.Models;

public class ApiHelper
{
    public static async Task<string> GetAll()
    {
        // Update this to the correct API URL
        RestClient client = new  RestClient("http://localhost:5000/"); // Updated to use HTTPS and correct port
        RestRequest request = new RestRequest($"api/destinations", Method.Get);
        RestResponse response = await client.GetAsync(request);
         // Log the response content
        if (string.IsNullOrEmpty(response.Content))
        {
            throw new Exception("API call returned an empty or null result.");
        }

        return response.Content;
    }

        /*
            EXPLAINING THE METHOD
                When writing a client for an api, both the APi and the client should be up and running n different ports; thats a a configuration we did in our launchSettings.json file to avoid errors
                Your API call should include the port that TravelApi is set to listen to. We already did those configs in our launchSettings.json file
                If you do choose to deploy the project, you will need to update the URL to include the domain of the deployed site instead of localhost. The endpoint itself for this particular call will be api/destinations.
                
                Previously we used the ExecuteAsync() method when making api calls, now we are using GetAsync() the only advantage of GetAsync() for this is that it will throw an error if the server returns an error to us. This is important if we want to create a robust frontend applicaion,
                We'll also be using PutAsync(), PostAsync(), DeletAsync()
        */

    public static async Task<string> Get(int id)
    {
        RestClient client = new RestClient("http://localhost:5000/");
        RestRequest request = new RestRequest($"api/destinations/{id}", Method.Get);
        RestResponse response = await client.GetAsync(request);
        return response.Content;

    }

    // public static async void Post(string newDestination)
    // {
    //     RestClient client = new RestClient("http://localhost:5000/");
    //     RestRequest request = new RestRequest($"api/destinations", Method.Post);
    //     request.AddHeader("Content-Type", "application/json");
    //     request.AddJsonBody(newDestination);
    //     await client.PostAsync(request);

    // }

/*
    public static async void Post(string newDestination)
    {
        RestClient client = new RestClient("http://localhost:5000/");  // Correct the port and use HTTPS
        RestRequest request = new RestRequest($"api/destinations", Method.Post);
        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(newDestination);
        RestResponse response = await client.PostAsync(request);

        if (!response.IsSuccessful)
        {
            throw new Exception($"Error: {response.StatusCode} - {response.Content}");
        }

    }

    */
        // Something is super broken in the code, and honestly I do nopt even know what that is.....
        // The Re

        /*
            EXPLAINING THE POST
                        The arguments passed into the RestRequest() method specify the route and method that should be passed into the API controller.
                        When making a POST request to our API (or any request that will be modifying our database), we need to add a header and a body. This way, our API can recognize the data types it receives and pass in the right argument for the controller route parameter(s).
                        We're also using the PostAsync() method, which will throw on a server error

        */

    // public static async void Put(int id, string newDestination)
    // {
    //     RestClient client = new RestClient("http://localhost:5000/");
    //     RestRequest request = new RestRequest($"api/destinations/{id}", Method.Put);
    //     request.AddHeader("Content-Type", "application/json");
    //     request.AddJsonBody(newDestination);
    //     await client.PutAsync(request);
    // }

/*
    public static async void Put(int id, string newDestination)
    {
        RestClient client = new RestClient("http://localhost:5000/");  // Ensure correct base URL
        RestRequest request = new RestRequest($"api/destinations/{id}", Method.Put);
        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(newDestination);  // Ensure newDestination is serialized properly
        RestResponse response = await client.PutAsync(request);

        if (!response.IsSuccessful)
        {
            throw new Exception($"Error: {response.StatusCode} - {response.Content}");
        }
    }
*/

    public static async Task Post(string newDestination)
    {
        RestClient client = new RestClient("http://localhost:5000/");  // Correct the port and use HTTPS
        RestRequest request = new RestRequest($"api/destinations", Method.Post);
        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(newDestination);
        RestResponse response = await client.PostAsync(request);

        if (!response.IsSuccessful)
        {
            throw new Exception($"Error: {response.StatusCode} - {response.Content}");
        }
    }

    public static async Task Put(int id, string newDestination)
    {
        RestClient client = new RestClient("http://localhost:5000/");  // Ensure correct base URL
        RestRequest request = new RestRequest($"api/destinations/{id}", Method.Put);
        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(newDestination);  // Ensure newDestination is serialized properly
        RestResponse response = await client.PutAsync(request);

        if (!response.IsSuccessful)
        {
            throw new Exception($"Error: {response.StatusCode} - {response.Content}");
        }
    }




    public static async void Delete(int id)
    {
        RestClient client = new RestClient("http://localhost:5000/");
        RestRequest request = new RestRequest($"api/destinations/{id}", Method.Delete);
        request.AddHeader("Content-Type", "application/json");
        await client.DeleteAsync(request);
    }
}