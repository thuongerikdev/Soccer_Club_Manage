﻿using SM.Constant.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Domain.Minigame
{
    [Table(nameof(Predictions), Schema = DbSchema.Tournament)]
    public  class Predictions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PredictionID { get; set; }
        public int MinigameID { get; set; }
        public int MatchID { get; set; }
        public int UserID { get; set; }
        public string Prediction { get; set; }
        public DateTime PredictionDate { get; set; }

    }
}
