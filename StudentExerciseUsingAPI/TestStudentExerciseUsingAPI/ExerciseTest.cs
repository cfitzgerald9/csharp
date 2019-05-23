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

    public class TestExercises
    {


        public async Task<Exercise> createNutshell(HttpClient client)
        {
            Exercise Nutshell = new Exercise
            {
                exerciseName = "Nutshell",
                exerciseLanguage = "React"

            };
            string NutshellAsJSON = JsonConvert.SerializeObject(Nutshell);


            HttpResponseMessage response = await client.PostAsync(
                "api/Exercise",
                new StringContent(NutshellAsJSON, Encoding.UTF8, "application/json")
            );

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            Exercise newNutshell = JsonConvert.DeserializeObject<Exercise>(responseBody);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            return newNutshell;

        }

        // Delete a Exercise in the database and make sure we get a no content status code back
        public async Task deleteNutshell(Exercise Nutshell, HttpClient client)
        {
            HttpResponseMessage deleteResponse = await client.DeleteAsync($"api/Exercise/{Nutshell.Id}");
            deleteResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        }


        [Fact]
        public async Task Test_Get_All_Exercises()
        {
            // Use the http client
            using (HttpClient client = new APIClientProvider().Client)
            {

                // Call the route to get all our Exercises; wait for a response object
                HttpResponseMessage response = await client.GetAsync("api/Exercise");

                // Make sure that a response comes back at all
                response.EnsureSuccessStatusCode();

                // Read the response body as JSON
                string responseBody = await response.Content.ReadAsStringAsync();

                // Convert the JSON to a list of Exercise instances
                List<Exercise> ExerciseList = JsonConvert.DeserializeObject<List<Exercise>>(responseBody);

                // Did we get back a 200 OK status code?
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                // Are there any Exercises in the list?
                Assert.True(ExerciseList.Count > 0);
            }
        }

        [Fact]
        public async Task Test_Get_Single_Exercise()
        {

            using (HttpClient client = new APIClientProvider().Client)
            {

                // Create a new Exercise
                Exercise newNutshell = await createNutshell(client);

                // Try to get that Exercise from the database
                HttpResponseMessage response = await client.GetAsync($"api/Exercise/{newNutshell.Id}");

                response.EnsureSuccessStatusCode();

                // Turn the response into JSON
                string responseBody = await response.Content.ReadAsStringAsync();

                // Turn the JSON into C#
                Exercise Exercise = JsonConvert.DeserializeObject<Exercise>(responseBody);

                // Did we get back what we expected to get back? 
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal("Nutshell", newNutshell.exerciseName);
                Assert.Equal("React", newNutshell.exerciseLanguage);

                // Clean up after ourselves- delete Nutshell!
                deleteNutshell(newNutshell, client);
            }
        }

        [Fact]
        public async Task Test_Get_NonExitant_Exercise_Fails()
        {

            using (var client = new APIClientProvider().Client)
            {
                // Try to get a Exercise with an enormously huge Id
                HttpResponseMessage response = await client.GetAsync("api/Exercise/999999999");

                // It should bring back a 204 no content error
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            }
        }


        [Fact]
        public async Task Test_Create_And_Delete_Exercise()
        {
            using (var client = new APIClientProvider().Client)
            {

                // Create a new Nutshell
                Exercise newNutshell = await createNutshell(client);

                // Make sure its info checks out
                Assert.Equal("Nutshell", newNutshell.exerciseName);
                Assert.Equal("React", newNutshell.exerciseLanguage);


                // Clean up after ourselves - delete Nutshell!
                deleteNutshell(newNutshell, client);
            }
        }

        [Fact]
        public async Task Test_Delete_NonExistent_Exercise_Fails()
        {
            using (var client = new APIClientProvider().Client)
            {
                // Try to delete an Id that shouldn't exist in the DB
                HttpResponseMessage deleteResponse = await client.DeleteAsync("/api/Exercise/600000");
                Assert.False(deleteResponse.IsSuccessStatusCode);
                Assert.Equal(HttpStatusCode.NotFound, deleteResponse.StatusCode);
            }
        }

        [Fact]
        public async Task Test_Modify_Exercise()
        {

            // We're going to change an Exercise's name! This is its new name.
            string newExerciseName = "Super cool...exercise?";

            using (HttpClient client = new APIClientProvider().Client)
            {

                // Create a new Exercise
                Exercise newNutshell = await createNutshell(client);

                // Change its first name
                newNutshell.exerciseName = newExerciseName;

                // Convert them to JSON
                string modifiedNutshellAsJSON = JsonConvert.SerializeObject(newNutshell);

                // Make a PUT request with the new info
                HttpResponseMessage response = await client.PutAsync(
                    $"api/Exercise/{newNutshell.Id}",
                    new StringContent(modifiedNutshellAsJSON, Encoding.UTF8, "application/json")
                );


                response.EnsureSuccessStatusCode();

                // Convert the response to JSON
                string responseBody = await response.Content.ReadAsStringAsync();

                // We should have gotten a no content status code
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

                /*
                    GET section
                 */
                // Try to GET the Exercise we just edited
                HttpResponseMessage getNutshell = await client.GetAsync($"api/Exercise/{newNutshell.Id}");
                getNutshell.EnsureSuccessStatusCode();

                string getNutshellBody = await getNutshell.Content.ReadAsStringAsync();
                Exercise modifiedNutshell = JsonConvert.DeserializeObject<Exercise>(getNutshellBody);

                Assert.Equal(HttpStatusCode.OK, getNutshell.StatusCode);

                // Make sure its name was in fact updated
                Assert.Equal(newExerciseName, modifiedNutshell.exerciseName);

                // Clean up after ourselves- delete it
                deleteNutshell(modifiedNutshell, client);
            }
        }
    }
}
