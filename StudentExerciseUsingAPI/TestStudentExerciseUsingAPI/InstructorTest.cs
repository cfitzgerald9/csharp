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

    public class TestInstructors
    {

        // Since we need to clean up after ourselves, we'll create and delete a Instructor when we test POST and PUT
        // Otherwise, eveyr time we ran our test suite it would create a new Jordan entry and we'd end up with a tooon of Jordans

        // Create a new Instructor in the db and make sure we get a 200 OK status code back
        public async Task<Instructor> createJordan(HttpClient client)
        {
             Instructor Jordan = new Instructor
            {
                firstName = "Jordan",
                lastName = "Castelloe",
                cohortId = 1,
                slackHandle = "@Jordan"
            };
            string JordanAsJSON = JsonConvert.SerializeObject(Jordan);


            HttpResponseMessage response = await client.PostAsync(
                "api/Instructor",
                new StringContent(JordanAsJSON, Encoding.UTF8, "application/json")
            );

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            Instructor newJordan = JsonConvert.DeserializeObject<Instructor>(responseBody);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            return newJordan;

        }

        // Delete a Instructor in the database and make sure we get a no content status code back
        public async Task deleteJordan(Instructor Jordan, HttpClient client)
        {
            HttpResponseMessage deleteResponse = await client.DeleteAsync($"api/Instructor/{Jordan.Id}");
            deleteResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        }


        [Fact]
        public async Task Test_Get_All_Instructors()
        {
            // Use the http client
            using (HttpClient client = new APIClientProvider().Client)
            {

                // Call the route to get all our Instructors; wait for a response object
                HttpResponseMessage response = await client.GetAsync("api/Instructor");

                // Make sure that a response comes back at all
                response.EnsureSuccessStatusCode();

                // Read the response body as JSON
                string responseBody = await response.Content.ReadAsStringAsync();

                // Convert the JSON to a list of Instructor instances
                List<Instructor> InstructorList = JsonConvert.DeserializeObject<List<Instructor>>(responseBody);

                // Did we get back a 200 OK status code?
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                // Are there any Instructors in the list?
                Assert.True(InstructorList.Count > 0);
            }
        }

        [Fact]
        public async Task Test_Get_Single_Instructor()
        {

            using (HttpClient client = new APIClientProvider().Client)
            {

                // Create a new Instructor
                Instructor newJordan = await createJordan(client);

                // Try to get that Instructor from the database
                HttpResponseMessage response = await client.GetAsync($"api/Instructor/{newJordan.Id}");

                response.EnsureSuccessStatusCode();

                // Turn the response into JSON
                string responseBody = await response.Content.ReadAsStringAsync();

                // Turn the JSON into C#
                Instructor Instructor = JsonConvert.DeserializeObject<Instructor>(responseBody);

                // Did we get back what we expected to get back? 
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal("Jordan", newJordan.firstName);
                Assert.Equal("Castelloe", newJordan.lastName);

                // Clean up after ourselves- delete Jordan!
                deleteJordan(newJordan, client);
            }
        }

        [Fact]
        public async Task Test_Get_NonExitant_Instructor_Fails()
        {

            using (var client = new APIClientProvider().Client)
            {
                // Try to get a Instructor with an enormously huge Id
                HttpResponseMessage response = await client.GetAsync("api/Instructor/999999999");

                // It should bring back a 204 no content error
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            }
        }


        [Fact]
        public async Task Test_Create_And_Delete_Instructor()
        {
            using (var client = new APIClientProvider().Client)
            {

                // Create a new Jordan
                Instructor newJordan = await createJordan(client);

                // Make sure his info checks out
                Assert.Equal("Jordan", newJordan.firstName);
                Assert.Equal("Castelloe", newJordan.lastName);
                Assert.Equal("@Jordan", newJordan.slackHandle);

                // Clean up after ourselves - delete Jordan!
                deleteJordan(newJordan, client);
            }
        }

        [Fact]
        public async Task Test_Delete_NonExistent_Instructor_Fails()
        {
            using (var client = new APIClientProvider().Client)
            {
                // Try to delete an Id that shouldn't exist in the DB
                HttpResponseMessage deleteResponse = await client.DeleteAsync("/api/Instructor/600000");
                Assert.False(deleteResponse.IsSuccessStatusCode);
                Assert.Equal(HttpStatusCode.NotFound, deleteResponse.StatusCode);
            }
        }

        [Fact]
        public async Task Test_Modify_Instructor()
        {

            // We're going to change a Instructor's name! This is their new name.
            string newFirstName = "Super cool lady";

            using (HttpClient client = new APIClientProvider().Client)
            {

                // Create a new Instructor
                Instructor newJordan = await createJordan(client);

                // Change their first name
                newJordan.firstName = newFirstName;

                // Convert them to JSON
                string modifiedJordanAsJSON = JsonConvert.SerializeObject(newJordan);

                // Make a PUT request with the new info
                HttpResponseMessage response = await client.PutAsync(
                    $"api/Instructor/{newJordan.Id}",
                    new StringContent(modifiedJordanAsJSON, Encoding.UTF8, "application/json")
                );


                response.EnsureSuccessStatusCode();

                // Convert the response to JSON
                string responseBody = await response.Content.ReadAsStringAsync();

                // We should have gotten a no content status code
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

                /*
                    GET section
                 */
                // Try to GET the Instructor we just edited
                HttpResponseMessage getJordan = await client.GetAsync($"api/Instructor/{newJordan.Id}");
                getJordan.EnsureSuccessStatusCode();

                string getJordanBody = await getJordan.Content.ReadAsStringAsync();
                Instructor modifiedJordan = JsonConvert.DeserializeObject<Instructor>(getJordanBody);

                Assert.Equal(HttpStatusCode.OK, getJordan.StatusCode);

                // Make sure his name was in fact updated
                Assert.Equal(newFirstName, modifiedJordan.firstName);

                // Clean up after ourselves- delete him
                deleteJordan(modifiedJordan, client);
            }
        }
    }
}