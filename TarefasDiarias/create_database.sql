/* RUN: psql -U postgres -f create_database.sql */

CREATE ROLE user1 LOGIN
    PASSWORD 'user1';

CREATE DATABASE tarefas_db;

\c tarefas_db

CREATE TABLE public.tasks_table
(
    id character varying(255) COLLATE pg_catalog."default" NOT NULL,
    title character varying(50) COLLATE pg_catalog."default",
    details character varying(100) COLLATE pg_catalog."default",
    creationdate timestamp without time zone,
    finaldate timestamp without time zone,
    isdone boolean,
    CONSTRAINT tasks_table_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.tasks_table
    OWNER to user1;
