using App.Data.Criterias.Core;
using App.Data.Entities.Tasks;
using App.Interfaces.RequestsParams;

namespace App.Data.Criterias.Tasks {
    public class TaskByKeyWord : KeyWordCriteria<TaskEntity> {
        public TaskByKeyWord(KeyWordFilter[] keywords) {
            Keywords = keywords;
        }
    }
}