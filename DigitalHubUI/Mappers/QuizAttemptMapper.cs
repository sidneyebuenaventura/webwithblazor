using DigitalHubUI.Dto;
using DigitalHubUI.ViewModels;

namespace DigitalHubUI.Mappers;

public static class QuizAttemptMapper
{
    /* Build QuizAttemptViewModel */
    public static QuizAttemptViewModel Map(QuizAttemptDto quizAttemptDto, IList<QuizAnswerDto> quizAnswerDtos, 
        IList<QuizQuestionDto> quizQuestionDtos, IList<QuizQuestionOptionDto> quizQuestionOptionDtos)
    {
        var (id, 
            attemptDate, 
            totalScore) = GetQuizAttemptDetails(quizAttemptDto);

        return new QuizAttemptViewModel(id, attemptDate, totalScore, 
            quizAnswerDtos.Select(Map).ToList(), 
            quizQuestionDtos.Select(qq => Map(qq, quizQuestionOptionDtos, quizAnswerDtos)).ToList());
    }
    
    private static (string id, 
        DateTime attemptDate, 
        int totalScore
        ) GetQuizAttemptDetails(QuizAttemptDto quizAttemptDto)
    {
        return (
            id: quizAttemptDto.Id,
            attemptDate: quizAttemptDto.AttemptDate,
            totalScore: quizAttemptDto.TotalScore.HasValue ? quizAttemptDto.TotalScore.Value : 0
        );
    }
    
    /* Build QuizAnswerViewModel */
    public static QuizAnswerViewModel Map(QuizAnswerDto quizAnswerDto)
    {
        var (id, 
            quizQuestionId, 
            selectedOptionId) = GetQuizAnswerDetails(quizAnswerDto);

        return new QuizAnswerViewModel(id, selectedOptionId, quizQuestionId);
    }
    
    private static (string id, 
        string quizQuestionId, 
        string selectedOptionId
        ) GetQuizAnswerDetails(QuizAnswerDto quizAnswerDto)
    {
        return (
            id: quizAnswerDto.Id,
            quizQuestionId: quizAnswerDto.QuizQuestion.FirstOrDefault().Id,
            selectedOptionId: quizAnswerDto.SelectedOption.FirstOrDefault().Id
        );
    }
    
    /* Build QuizQuestionAttemptViewModel */
    public static QuizQuestionAttemptViewModel Map(QuizQuestionDto quizQuestionDto, IList<QuizQuestionOptionDto> quizQuestionOptionDtos,
        IList<QuizAnswerDto> quizAnswerDtos)
    {
        var (id,
            title, 
            content, 
            order) = GetQuizQuestionAttemptDetails(quizQuestionDto);

        return new QuizQuestionAttemptViewModel(id,
            title, 
            content, 
            order, 
            new List<QuizQuestionOptionViewModel>(),
                   quizQuestionOptionDtos
                           // filter only quizQuestionDto belong to current quizQuestionDto
                       .Where(qqo => qqo.ParentId.Equals(quizQuestionDto.Id))
                       .Select(qqo => Map(qqo, quizAnswerDtos)).ToList());
    }
    
    private static (string id,
        string title, 
        string content, 
        int order
        ) GetQuizQuestionAttemptDetails(QuizQuestionDto quizQuestionDto)
    {
        return (
            id: quizQuestionDto.Id,
            title: quizQuestionDto.Title,
            content: quizQuestionDto.Content,
            order: quizQuestionDto.Order
        );
    }
    
    /* Build QuizQuestionOptionAttemptViewModel */
    public static QuizQuestionOptionAttemptViewModel Map(QuizQuestionOptionDto quizQuestionOptionDto, IList<QuizAnswerDto> quizAnswerDtos)
    {
        var (id,
            content, 
            isAnswer, 
            order) = GetQuizQuestionOptionAttemptDetails(quizQuestionOptionDto);

        return new QuizQuestionOptionAttemptViewModel(id,
            content, 
            isAnswer, 
            order, 
            // check if option is selected which means selectedOptionIds containing current optionId
            quizAnswerDtos.Select(qa => qa.SelectedOption.FirstOrDefault().Id).ToList()
                .Contains(quizQuestionOptionDto.Id));
    }
    
    private static (string id,
        string content, 
        bool isAnswer, 
        int order
        ) GetQuizQuestionOptionAttemptDetails(QuizQuestionOptionDto quizQuestionOptionDto)
    {
        return (
            id: quizQuestionOptionDto.Id,
            content: quizQuestionOptionDto.Content,
            isAnswer: quizQuestionOptionDto.IsAnswer,
            order: quizQuestionOptionDto.Order
        );
    }
}