using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using TravelClient.Models;


namespace TravelClient.Models;
public class Destination
{
    [Required]
    public int DestinationId { get; set; }

    [Required]
    [StringLength(20)]
    public string UserName { get; set; }

    [Required]
    public string Country { get; set; }
    
    [Required]
    public string DestinationName { get; set; }

    [Required]
    public string Review { get; set; }

    [Required]
    [Range(1, 5, ErrorMessage = " All destinations must have a rating between 1-5 ")]
    public int OverallRating { get; set; }
    

    public static List<Destination> GetDestinations()
    {
        var apiCallTask = ApiHelper.GetAll();
        var result = apiCallTask.Result;

        if (string.IsNullOrEmpty(result))
        {
            // Handle the case where the API call returned an empty or null result
            throw new Exception("API call returned an empty or null result.");
        }

        try
        {
            // Log the result to verify the content of the API response
            Console.WriteLine("API response: " + result);

            // Ensure the result is a valid JSON array
            JArray jsonResponse = JArray.Parse(result); // Use Parse instead of DeserializeObject for better error handling

            if (jsonResponse == null)
            {
                throw new Exception("Deserialization of the API response returned null.");
            }

            List<Destination> destinationList = jsonResponse.ToObject<List<Destination>>();

            return destinationList;
        }
        catch (JsonReaderException jsonEx)
        {
            // Catch and handle specific JSON parsing errors
            throw new Exception("Invalid JSON format in API response: " + jsonEx.Message);
        }
        catch (Exception ex)
        {
            // Catch any other exceptions that may occur
            throw new Exception("Error deserializing the API response: " + ex.Message);
        }
    }

        /*

        Explaining the METHOD:
            The method handles calling a method that queries our API for all Destination objects and deserialize the API's response
            We need a different Destination class and ApiHelper class method for each type of API call that we want to make(GET, POST, PUT, DELETE) we want to make because each returns a different format of data
           NO API key was passed as an argument when doing ApiHelper.GetAll()... This is because your personal API will not require a key unless you add Token-Based Authentication
            
            Next, take note that the jsonResponse variable is of the type JArray as opposed to JObject. Since we're getting a collection of results, we need to expect an array of objects. Remember that these types are from the Newtonsoft.Json library.
            Next, let's actually create the ApiHelper class. This class will contain the definition for our ApiHelper.GetAll() method which actually handles making a call to our Travel Api

        */
    public static Destination GetDetails(int id)
    {
        var apiCallTask = ApiHelper.Get(id);
        var result = apiCallTask.Result;

        JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
        Destination destination = JsonConvert.DeserializeObject<Destination>(jsonResponse.ToString());

        return destination;
        // the jsonResponse variable is of the type JObject ad this is because we are getting just a single object. The type JObject is also from the Newtonsoft.json namespace
    }

    public static void Post(Destination dest)
    {
        string jsonDestination = JsonConvert.SerializeObject(dest);
        ApiHelper.Post(jsonDestination);
    }

    public static void Put(Destination dest)
    {
        string jsonDestination = JsonConvert.SerializeObject(dest);
        ApiHelper.Put(dest.DestinationId, jsonDestination);
    }

    public static void Delete(int id)
    {
        ApiHelper.Delete(id);
    }

}