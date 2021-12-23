﻿using System.Collections.Generic;
using Models.Api.Components;

namespace Models.Api
{
    public class Movie : IResult, Content
    {
        public int id { get; set; }
        public string title { get; set; }
        public string overview { get; set; }
        public int runtime { get; set; }
        public string release_date { get; set; }
        public bool adult { get; set; }
        public List<Genre> genres { get; set; }
        public MovieReleaseDates release_dates { get; set; }
        public List<int> genre_ids { get; set; }

        public bool video { get; set; }
        public string poster_path { get; set; }
        public string backdrop_path { get; set; }
        public Videos videos { get; set; }
        public Images images { get; set; }

        public double vote_average { get; set; }
        public int vote_count { get; set; }
        public double popularity { get; set; }

        public string imdb_id { get; set; }
        public string homepage { get; set; }
        public string tagline { get; set; }
        public int budget { get; set; }
        public ulong revenue { get; set; }
        public string status { get; set; }
        public string media_type { get; set; } = "movie";
        public BelongsToCollection belongs_to_collection { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public List<ProductionCompany> production_companies { get; set; }
        public List<ProductionCountry> production_countries { get; set; }
        public List<SpokenLanguage> spoken_languages { get; set; }
        public Credits credits { get; set; }

        public Movie(string poster_path)
        {
            this.poster_path = poster_path;
        }
    }
}