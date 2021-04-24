using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevQuiz.Libraries.Core.Models.Dto;

namespace DevQuiz.Libraries.Services.Dto
{
    /// <summary>
    /// Question Dto model
    /// </summary>
    public class QuestionDto : QuestionDtoBase<AnswerDto, CategoryDto, TagDto>
    {
    }
}
