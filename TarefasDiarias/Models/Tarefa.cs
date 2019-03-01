using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TarefasDiarias.Models {
    public class Tarefa {
        public virtual string Id { get; set; }
        public virtual string Titulo { get; set; }
        public virtual string Detalhes { get; set; }
        
        [Display(Name = "Criada em")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public virtual DateTime DataCriacao { get; set; }

        [Display(Name = "Data de execução")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public virtual DateTime DataExecucao { get; set; }

        [Display(Name = "Horário a ser executada")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public virtual DateTime HorarioExecucao { get; set; }

        [Display(Name = "Tarefa foi executada?")]
        public virtual bool Concluida { get; set; }
    }
}