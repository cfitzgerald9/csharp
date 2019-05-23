using Newtonsoft.Json;
using StudentExerciseUsingAPI.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using TestStudentExerciseUsingAPI;

namespace TestStudentExerciseUsingAPI
{

    public class TestCohorts
    {

  
        public async Task<Cohort> createFive(HttpClient client)
        {
            Cohort Five = new Cohort
            {
                cohortName = "Five",
          
            };
            string FiveAsJSON = JsonConvert.SerializeObject(Five);


            HttpResponseMessage response = await client.PostAsync(
                "api/Cohort",
                new StringContent(FiveAsJSON, Encoding.UTF8, "application/json")
            );

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            Cohort newFive = JsonConvert.DeserializeObject<Cohort>(responseBody);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            return newFive;

        }

        // Delete a Cohort in the database and make sure we get a no content status code back
        public async Task deleteFive(Cohort Five, HttpClient client)
        {
            HttpResponseMessage deleteResponse = await client.DeleteAsync($"api/Cohort/{Five.Id}");
            deleteResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        }


        [Fact]
        public async Task Test_Get_All_Cohorts()
        {
            // Use the http client
            using (HttpClient client = new APIClientProvider().Client)
            {

                // Call the route to get all our Cohorts; wait for a response object
                HttpResponseMessage response = await client.GetAsync("api/Cohort");

                // Make sure that a response comes back at all
                response.EnsureSuccessStatusCode();

                // Read the response body as JSON
                string responseBody = await response.Content.ReadAsStringAsync();

                // Convert the JSON to a list of Cohort instances
                List<Cohort> CohortList = JsonConvert.DeserializeObject<List<Cohort>>(responseBody);

                // Did we get back a 200 OK status code?
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                // Are there any Cohorts in the list?
                Assert.True(CohortList.Count > 0);
            }
        }

        [Fact]
        public async Task Test_Get_Single_Cohort()
        {

            using (HttpClient client = new APIClientProvider().Client)
            {

                // Create a new Cohort
                Cohort newFive = await createFive(client);

                // Try to get that Cohort from the database
                HttpResponseMessage response = await client.GetAsync($"api/Cohort/{newFive.Id}");

                response.EnsureSuccessStatusCode();

                // Turn the response into JSON
                string responseBody = await response.Content.ReadAsStringAsync();

                // Turn the JSON into C#
                Cohort Cohort = JsonConvert.DeserializeObject<Cohort>(responseBody);

                // Did we get back what we expected to get back? 
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal("Five", newFive.cohortName);
              
                // Clean up after ourselves- delete Five!
                deleteFive(newFive, client);
            }
        }

        [Fact]
        public async Task Test_Get_NonExitant_Cohort_Fails()
        {

            using (var client = new APIClientProvider().Client)
            {
                // Try to get a Cohort with an enormously huge Id
                HttpResponseMessage response = await client.GetAsync("api/Cohort/999999999");

                // It should bring back a 204 no content error
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            }
        }


        [Fact]
        public async Task Test_Create_And_Delete_Cohort()
        {
            using (var client = new APIClientProvider().Client)
            {

                // Create a new Five
                Cohort newFive = await createFive(client);

                // Make sure his info checks out
                Assert.Equal("Five", newFive.cohortName);


                // Clean up after ourselves - delete Five!
                deleteFive(newFive, client);
            }
        }

        [Fact]
        public async Task Test_Delete_NonExistent_Cohort_Fails()
        {
            using (var client = new APIClientProvider().Client)
            {
                // Try to delete an Id that shouldn't exist in the DB
                HttpResponseMessage deleteResponse = await client.DeleteAsync("/api/Cohort/600000");
                Assert.False(deleteResponse.IsSuccessStatusCode);
                Assert.Equal(HttpStatusCode.NotFound, deleteResponse.StatusCode);
            }
        }

        [Fact]
        public async Task Test_Modify_Cohort()
        {

            // We're going to change a Cohort's name! This is their new name.
            string newCohortName = "Super cool...group of people?";

            using (HttpClient client = new APIClientProvider().Client)
            {

                // Create a new Cohort
                Cohort newFive = await createFive(client);

                // Change their first name
                newFive.cohortName = newCohortName;

                // Convert them to JSON
                string modifiedFiveAsJSON = JsonConvert.SerializeObject(newFive);

                // Make a PUT request with the new info
                HttpResponseMessage response = await client.PutAsync(
                    $"api/Cohort/{newFive.Id}",
                    new StringContent(modifiedFiveAsJSON, Encoding.UTF8, "application/json")
                );


                response.EnsureSuccessStatusCode();

                // Convert the response to JSON
                string responseBody = await response.Content.ReadAsStringAsync();

                // We should have gotten a no content status code
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

                /*
                    GET section
                 */
                // Try to GET the Cohort we just edited
                HttpResponseMessage getFive = await client.GetAsync($"api/Cohort/{newFive.Id}");
                getFive.EnsureSuccessStatusCode();

                string getFiveBody = await getFive.Content.ReadAsStringAsync();
                Cohort modifiedFive = JsonConvert.DeserializeObject<Cohort>(getFiveBody);

                Assert.Equal(HttpStatusCode.OK, getFive.StatusCode);

                // Make sure his name was in fact updated
                Assert.Equal(newCohortName, modifiedFive.cohortName);

                // Clean up after ourselves- delete him
                deleteFive(modifiedFive, client);
            }
        }
    }
}