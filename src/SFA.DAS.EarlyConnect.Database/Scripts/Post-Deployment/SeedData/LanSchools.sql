 IF NOT EXISTS (SELECT 1 FROM DatabaseHistory WHERE ScriptName = 'LanSchools.sql')
 BEGIN
    INSERT INTO EducationalOrganisation (
        Name, 
        EducationalType, 
        AddressLine1, 
        AddressLine2, 
        AddressLine3, 
        Town, 
        County, 
        Postcode, 
        URN,
	    LepCode
    ) VALUES 
    ('Accrington St Christophers Church of England High School', 'Academies', 'Queens Road West', '', '', 'Accrington', 'Lancashire', 'BB5 4AY', 137421,'E37000019'),
    ('Alder Grange School', 'Local authority maintained schools', 'Calder Road', 'Rawtenstall', '', 'Rossendale', 'Lancashire', 'BB4 8HW', 119722,'E37000019'),
    ('Bacup and Rawtenstall Grammar School', 'Academies', 'Glen Road', '', '', 'Rossendale', 'Lancashire', 'BB4 7BJ', 150261,'E37000019'),
    ('Blackburn College', 'Colleges', 'Blackburn College', 'Harrison Centre', 'Feilden Street', 'Blackburn', 'Lancashire', 'BB2 1LH', 130736,'E37000019'),
    ('Blackpool and the Fylde College', 'Colleges', 'Ashfield Road', 'Bispham', '', 'Blackpool', 'Lancashire', 'FY2 0HB', 130739,'E37000019'),
    ('Burnley College', 'Colleges', 'Princess Way', '', '', 'Burnley', 'Lancashire', 'BB12 0AN', 130735,'E37000019'),
    ('Cardinal Newman College', 'Colleges', 'Lark Hill Road', '', 'Avenham', 'Preston', 'Lancashire', 'PR1 4HD', 130745,'E37000019'),
    ('Clitheroe Royal Grammar School', 'Academies', 'York Street', '', '', 'Clitheroe', 'Lancashire', 'BB7 2DJ', 136390,'E37000019'),
    ('Darwen Aldridge Community Academy', 'Academies', 'Sudell Road', '', '', 'Darwen', 'Lancashire', 'BB3 3HD', 135580,'E37000019'),
    ('Darwen Aldridge Enterprise Studio', 'Free Schools', 'Police Street', '', '', 'Darwen', 'Lancashire', 'BB3 1AF', 139924,'E37000019'),
    ('Haslingden High School and Sixth Form', 'Local authority maintained schools', 'Broadway', 'Haslingden', '', 'Rossendale', 'Lancashire', 'BB4 4EY', 119767,'E37000019'),
    ('Hutton Church of England Grammar School', 'Local authority maintained schools', 'Liverpool Road', 'Hutton', '', 'Preston', 'Lancashire', 'PR4 5SN', 119794,'E37000019'),
    ('Lancaster and Morecambe College', 'Colleges', 'Morecambe Road', '', '', 'Lancaster', 'Lancashire', 'LA1 2TY', 130737,'E37000019'),
    ('Lancaster Girls Grammar School', 'Academies', 'Regent Street', '', '', 'Lancaster', 'Lancashire', 'LA1 1SF', 136381,'E37000019'),
    ('Lancaster Royal Grammar School', 'Academies', 'East Road', '', '', 'Lancaster', 'Lancashire', 'LA1 3EF', 136742,'E37000019'),
    ('Lancaster University School of Mathematics', 'Free Schools', '67-69 London Road', '', '', 'Preston', 'Lancashire', 'PR1 4BA', 149099,'E37000019'),
    ('Moor Park High School and Sixth Form', 'Local authority maintained schools', 'Moor Park Avenue', '', '', 'Preston', 'Lancashire', 'PR1 6DT', 119773,'E37000019'),
    ('Morecambe Bay Academy', 'Academies', 'Dallam Avenue', '', '', 'Morecambe', 'Lancashire', 'LA4 5BG', 147115,'E37000019'),
    ('Myerscough College', 'Colleges', 'St Michaels Road', 'Bilsborrow', '', 'Preston', 'Lancashire', 'PR3 0RY', 130743,'E37000019'),
    ('Nelson and Colne College', 'Colleges', 'Scotland Road', '', '', 'Nelson', 'Lancashire', 'BB9 7YT', 130738,'E37000019'),
    ('Ormskirk School', 'Academies', 'Wigan Road', '', '', 'Ormskirk', 'Lancashire', 'L39 2AT', 148254,'E37000019'),
    ('Our Ladys Catholic College', 'Local authority maintained schools', 'Morecambe Road', '', '', 'Lancaster', 'Lancashire', 'LA1 2RX', 119798,'E37000019'),
    ('Preston College', 'Colleges', 'Fulwood Campus', 'St Vincent''s Road', 'Fulwood', 'Preston', 'Lancashire', 'PR2 8UR', 130740,'E37000019'),
    ('Queen Elizabeths Grammar School', 'Free Schools', 'West Park Road', '', '', 'Blackburn', 'Lancashire', 'BB2 6DF', 141165,'E37000019'),
    ('Ripley St Thomas Church of England Academy', 'Academies', 'Ashton Road', '', '', 'Lancaster', 'Lancashire', 'LA1 4RS', 136731,'E37000019'),
    ('St Marys Catholic Academy', 'Academies', 'St Walburgas Road', '', '', 'Blackpool', 'Lancashire', 'FY3 7EQ', 141257,'E37000019'),
    ('St Wilfrids Church of England Academy', 'Academies', 'Duckworth Street', '', '', 'Blackburn', 'Lancashire', 'BB2 2JR', 136900,'E37000019'),
    ('Tauheedul Islam Boys High School', 'Free Schools', 'Sumner Street', '', '', 'Blackburn', 'Lancashire', 'BB2 2LD', 138220,'E37000019'),
    ('Tauheedul Islam Girls High School', 'Academies', 'Preston New Road', '', '', 'Blackburn', 'Lancashire', 'BB2 7AD', 141565,'E37000019'),
    ('The Blackpool Sixth Form College', 'Further education', 'Blackpool Old Road', '', '', 'Blackpool', 'Lancashire', 'FY3 7LR', 130744,'E37000019'),
    ('West Lancashire College', 'Further education', 'College Way', '', '', 'Skelmersdale', 'Lancashire', 'WN8 6DX', 142478,'E37000019'),
    ('Accrington Academy', 'Academy sponsor led', 'Queens Road West', '', '', 'Accrington', 'Lancashire', 'BB5 4FF', 135649,'E37000019'),
    ('Park Community Academy', 'Academy special converter', '158 Whitegate Drive', '', '', 'Blackpool', 'Lancashire', 'FY3 9HF', 140143,'E37000019'),
    ('Tor View School', 'Academy special converter', 'Clod Lane', 'Haslingden', '', 'Rossendale', 'Lancashire', 'BB4 6LR', 143879,'E37000019');

INSERT INTO DatabaseHistory (ScriptName) values ('LanSchools.sql')

END
