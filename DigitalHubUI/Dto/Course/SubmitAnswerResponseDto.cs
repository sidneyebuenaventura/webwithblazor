namespace DigitalHubUI.Dto.Course;

public class SubmitAnswerResponseDto
{
	public SubmitQuizQuestion[] QuizQuestions { get; set; }

	public SubmitAnswerResponseDto(SubmitQuizQuestion[] quizQuestions)
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

