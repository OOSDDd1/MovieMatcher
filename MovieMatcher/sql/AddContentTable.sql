﻿create table Matchmaker.content
(
    id            int                 not null,
    cache_key     int                 not null,
    title         varchar(255)        not null,
    overview      varchar(255)        not null,
    poster_path   varchar(255)        not null,
    backdrop_path varchar(255)        not null,
    trailer_url   varchar(255)        not null,
    age           int      default 12 not null,
    json          nvarchar(max)       not null,
    updated_at    datetime default getdate(),
    constraint content_pk
        primary key (cache_key, id)
)
go