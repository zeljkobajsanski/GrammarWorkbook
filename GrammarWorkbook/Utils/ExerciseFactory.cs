using System;
using GrammarWorkbook.Data.Models;

namespace GrammarWorkbook.Utils
{
    public static class ExerciseFactory
    {
        public static Exercise Create(string type)
        {
            switch (type)
            {
                    case "fill": return new FillTheBlanksExercise(){Id = Guid.NewGuid()};
                    default: throw new Exception("Not found exercise type");
            }
        }
    }
}