Assumptions made:

First names must start with a capital letter, there cannot be a second capital letter in the middle of the first name.

Manually tested with a .txt file since the allowed file formats were not specified in the requirements. To test, I launched the API, used Postman, selected form-data in the request body, and attached a .txt file with the key "file" to send it to the API and receive a response.

Implementation was done using a minimal API rather than a controller-based approach since the requirements did not specify which one was preferred.

Wasn't fully sure about the bonus points task, since valid result shouldn't output any lines, invalid result should output only invalid lines, yet timed lines sounds like it should output all the lines, so chose to have a custom response for it, with just value whether line was valid or not and it's time in ticks. Thus the endpoint has option "timed" query parameter to provide, for the main task requirements call "/validate/accounts" to get the result as provided in the task requirements or "/validate/accounts?timed=true" to see the custom timings result for each line.
