--
-- PostgreSQL database dump
--

-- Dumped from database version 16.2 (Debian 16.2-1.pgdg120+2)
-- Dumped by pg_dump version 16.2

-- Started on 2024-03-17 18:23:10 -03

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 3384 (class 1262 OID 16388)
-- Name: ultracar_db; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE ultracar_db WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en_US.utf8';


ALTER DATABASE ultracar_db OWNER TO postgres;

\connect ultracar_db

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 4 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: pg_database_owner
--

CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO pg_database_owner;

--
-- TOC entry 3385 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: pg_database_owner
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 217 (class 1259 OID 16395)
-- Name: Estoque; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Estoque" (
    "Id" integer NOT NULL,
    "NomePeca" text NOT NULL,
    "EstoquePeca" integer NOT NULL,
    "TipoMovimentacao" integer NOT NULL
);


ALTER TABLE public."Estoque" OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 16394)
-- Name: Estoque_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Estoque" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Estoque_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 219 (class 1259 OID 16403)
-- Name: Orcamentos; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Orcamentos" (
    "Id" integer NOT NULL,
    "NumeracaoOrcamento" text NOT NULL,
    "PlacaVeiculo" text NOT NULL,
    "NomeCliente" text NOT NULL
);


ALTER TABLE public."Orcamentos" OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 16402)
-- Name: Orcamentos_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Orcamentos" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Orcamentos_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 221 (class 1259 OID 16411)
-- Name: Pecas; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Pecas" (
    "Id" integer NOT NULL,
    "Quantidade" integer NOT NULL,
    "NomePeca" text NOT NULL,
    "OrcamentoId" integer,
    "EstoqueId" integer
);


ALTER TABLE public."Pecas" OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 16410)
-- Name: Pecas_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Pecas" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Pecas_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 215 (class 1259 OID 16389)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- TOC entry 3374 (class 0 OID 16395)
-- Dependencies: 217
-- Data for Name: Estoque; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Estoque" ("Id", "NomePeca", "EstoquePeca", "TipoMovimentacao") FROM stdin;
1	Peca1	100	0
2	Peca2	200	1
3	Peca3	2	1
4	Peca4	0	1
\.


--
-- TOC entry 3376 (class 0 OID 16403)
-- Dependencies: 219
-- Data for Name: Orcamentos; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Orcamentos" ("Id", "NumeracaoOrcamento", "PlacaVeiculo", "NomeCliente") FROM stdin;
1	112	ABC1234	John Doe
2	223	XYZ5678	Jane Smith
3	334	ABC1234	John Doe
\.


--
-- TOC entry 3378 (class 0 OID 16411)
-- Dependencies: 221
-- Data for Name: Pecas; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Pecas" ("Id", "Quantidade", "NomePeca", "OrcamentoId", "EstoqueId") FROM stdin;
1	1	Peca1	1	1
2	3	Peca2	2	2
3	2	Peca3	1	3
4	1	Peca4	3	4
\.


--
-- TOC entry 3372 (class 0 OID 16389)
-- Dependencies: 215
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20240317211901_InitialMigrations	8.0.3
\.


--
-- TOC entry 3386 (class 0 OID 0)
-- Dependencies: 216
-- Name: Estoque_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Estoque_Id_seq"', 4, true);


--
-- TOC entry 3387 (class 0 OID 0)
-- Dependencies: 218
-- Name: Orcamentos_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Orcamentos_Id_seq"', 3, true);


--
-- TOC entry 3388 (class 0 OID 0)
-- Dependencies: 220
-- Name: Pecas_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Pecas_Id_seq"', 4, true);


--
-- TOC entry 3220 (class 2606 OID 16401)
-- Name: Estoque PK_Estoque; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Estoque"
    ADD CONSTRAINT "PK_Estoque" PRIMARY KEY ("Id");


--
-- TOC entry 3222 (class 2606 OID 16409)
-- Name: Orcamentos PK_Orcamentos; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Orcamentos"
    ADD CONSTRAINT "PK_Orcamentos" PRIMARY KEY ("Id");


--
-- TOC entry 3226 (class 2606 OID 16417)
-- Name: Pecas PK_Pecas; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Pecas"
    ADD CONSTRAINT "PK_Pecas" PRIMARY KEY ("Id");


--
-- TOC entry 3218 (class 2606 OID 16393)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- TOC entry 3223 (class 1259 OID 16428)
-- Name: IX_Pecas_EstoqueId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Pecas_EstoqueId" ON public."Pecas" USING btree ("EstoqueId");


--
-- TOC entry 3224 (class 1259 OID 16429)
-- Name: IX_Pecas_OrcamentoId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Pecas_OrcamentoId" ON public."Pecas" USING btree ("OrcamentoId");


--
-- TOC entry 3227 (class 2606 OID 16418)
-- Name: Pecas FK_Pecas_Estoque_EstoqueId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Pecas"
    ADD CONSTRAINT "FK_Pecas_Estoque_EstoqueId" FOREIGN KEY ("EstoqueId") REFERENCES public."Estoque"("Id");


--
-- TOC entry 3228 (class 2606 OID 16423)
-- Name: Pecas FK_Pecas_Orcamentos_OrcamentoId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Pecas"
    ADD CONSTRAINT "FK_Pecas_Orcamentos_OrcamentoId" FOREIGN KEY ("OrcamentoId") REFERENCES public."Orcamentos"("Id");


-- Completed on 2024-03-17 18:23:10 -03

--
-- PostgreSQL database dump complete
--

