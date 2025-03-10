using GenerativeAI;
using GenerativeAI.Types;


var apiKey = "AIzaSyDfSjIDXSm0wBG6ne8qAbXBDNyJNQttf18";
var googleAI = new GoogleAi(apiKey);
var model = googleAI.CreateGenerativeModel("models/gemini-2.0-flash");
var config = new GenerationConfig()
{
    Temperature = 0.8f,
    MaxOutputTokens = 800
};
string docs = "In Vietnam, university admissions are primarily based on the High School Graduation Examination (HSGE) results, with specific requirements varying by institution and program. The HSGE includes compulsory subjects such as Vietnamese Literature, Mathematics, and a Foreign Language, along with elective subjects like Natural Sciences (Physics, Chemistry, Biology) or Social Sciences (History, Geography, Civics). Universities often consider combinations of these subjects, known as blocks, for admissions: Block A00: Mathematics, Physics, Chemistry; Block A01: Mathematics, Physics, English; Block C00: Literature, History, Geography; Block D01: Literature, Mathematics, English. Admission requirements differ among universities: FPT University: Requires academic transcripts with an average score above 6.5. HUTECH University: Also mandates academic transcripts with an average score above 6.5. Vietnam National University, Hanoi (VNU-HN): Requires HSGE results totaling above 27 points. Hanoi University of Science and Technology (HUST) requires HSGE result above 27.5";
string context = "You are an adviser. Here are your instruction: 1.You must condense your answer into about 800 words. 2. You will not use list, always in paragraph. 3. Be succinct. 4. You dont give me conclusion. 5. You must follow the provided docs if you see the query is related to it. 6. You dont give summary or conclusion again. 7. Your answer must be short to fit about 800 tokens. 8. DO not include the history of the previous answer into the new answer. 9. Use the history of previous queries to answer but not inlcude them in when answer.";

var history = new List<Content>
{
    new Content(docs, Roles.User),
    new Content(context, Roles.User),
};
var chatSession = model.StartChat(config: config, history: history);
string? userMessage;
userMessage = Console.ReadLine();

while (userMessage != "exit")
{
    context += userMessage;
    var firstResponse = await chatSession.GenerateContentAsync(context);
    Console.WriteLine("Response: " + firstResponse.Text());
    userMessage = Console.ReadLine();
}
