namespace DigitalHubUI.Dto.Course;

public class SubmitAnswerRequestDto
{
	public SubmitQuizQuestion[] QuizQuestions { get; set; }

	// No param constructor used for testing
	public SubmitAnswerRequestDto() {}
	
	public SubmitAnswerRequestDto(SubmitQuizQuestion[] quizQuestions)
	{
		QuizQuestions = quizQuestions;
	}

	public class SubmitQuizQuestion
	{
		public string Id { get; set; }
		public string[] QuizQuestionOptions { get; set; }

		public SubmitQuizQuestion(string id, string[] quizQuestionOptions)
		{
			Id = id;
			QuizQuestionOptions = quizQuestionOptions;
		}
	}

}

