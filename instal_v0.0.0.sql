do $$
begin

	--schema
	DROP SCHEMA IF EXISTS "DevQuiz" CASCADE;
	CREATE SCHEMA "DevQuiz";
	
	--tables
	CREATE TABLE "DevQuiz".category (
		id int4 NOT NULL GENERATED ALWAYS AS IDENTITY,
		name varchar NOT NULL,
		CONSTRAINT category_pkey PRIMARY KEY (id)
	);
	
	CREATE TABLE "DevQuiz".question (
		id int4 NOT NULL GENERATED ALWAYS AS IDENTITY,
		text varchar not null,
		category_id int null,
		CONSTRAINT question_pkey PRIMARY KEY (id),
		CONSTRAINT question_category_fkey FOREIGN KEY (category_id) REFERENCES "DevQuiz".category(id)
	);
	

	CREATE TABLE "DevQuiz".answer (
		id int4 NOT NULL GENERATED ALWAYS AS IDENTITY,
		text varchar not null,
		question int null,
		CONSTRAINT answer_pkey PRIMARY KEY (id)
	);

	CREATE TABLE "DevQuiz".tag (
		id int4 NOT NULL GENERATED ALWAYS AS IDENTITY,
		name varchar not null,
		CONSTRAINT tag_pkey PRIMARY KEY (id)
	);

	create table "DevQuiz".question_tag(
		id int4 NOT NULL GENERATED ALWAYS AS IDENTITY,
		question_id int not null,
		tag_id int not null,
		CONSTRAINT question_tag_pkey PRIMARY KEY (id),
		CONSTRAINT question_tag_question_fkey FOREIGN KEY (question_id) REFERENCES "DevQuiz".question(id),
		CONSTRAINT question_tag_tag_fkey FOREIGN KEY (tag_id) REFERENCES "DevQuiz".tag(id)
	);

	create table "DevQuiz".question_asnwer(
		id int4 NOT NULL GENERATED ALWAYS AS IDENTITY,
		is_right bool not null,
		question_id int not null,
		answer_id int not null,
		explanation text null,
		CONSTRAINT question_asnwer_pkey PRIMARY KEY (id),
		CONSTRAINT question_asnwer_question_fkey FOREIGN KEY (question_id) REFERENCES "DevQuiz".question(id),
		CONSTRAINT question_asnwer_answer_fkey FOREIGN KEY (answer_id) REFERENCES "DevQuiz".answer(id)
	);
end
$$