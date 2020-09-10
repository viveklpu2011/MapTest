﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Xaminals.Models
{
   public class ResponseCls
    {
    }
    public class wsGooglePlaces
    {
        public List<PredictionL> predictions { get; set; }
        public string status { get; set; }
    }
    public class MainTextMatchedSubstring
    {
        public int length { get; set; }
        public int offset { get; set; }
    }
    public class StructuredFormatting
    {
        public string main_text { get; set; }
        public List<MainTextMatchedSubstring> main_text_matched_substrings { get; set; }
        public string secondary_text { get; set; }
    }
    public class Term
    {
        public int offset { get; set; }
        public string value { get; set; }
    }
    public class MatchedSubstring
    {
        public int length { get; set; }
        public int offset { get; set; }
    }

    public class PredictionL
    {
        public string description { get; set; }
        public string id { get; set; }
        public List<MatchedSubstring> matched_substrings { get; set; }
        public string place_id { get; set; }
        public string reference { get; set; }
        public StructuredFormatting structured_formatting { get; set; }
        public List<Term> terms { get; set; }
        public List<string> types { get; set; }
    }
}
