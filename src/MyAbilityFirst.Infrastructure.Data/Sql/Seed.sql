-- ***************************************************************************************************************************
-- Seed Data
-- ***************************************************************************************************************************
insert into Suburb values ('Sydney','NSW', 2000)
insert into Suburb values ('Melbourne','VIC', 3000)
insert into Suburb values ('Brisbane','QLD', 4000)


declare @categoryname as varchar(255) 
declare @categoryID as int

set @categoryname = 'Gender'
insert into Category values (@categoryname)
	select @categoryID = ID from Category where Name = @categoryname
		insert into SubCategory values (@categoryid,'Male')
		insert into SubCategory values (@categoryid,'Female')
		insert into SubCategory values (@categoryid,'Other')
		insert into SubCategory values (@categoryid,'Not Disclosed')

set @categoryname = 'MarketingInfo'
insert into Category values (@categoryname)
	select @categoryID = ID from Category where Name = @categoryname
		insert into SubCategory values (@categoryid,'Existing Client')
		insert into SubCategory values (@categoryid,'Previous Client')
		insert into SubCategory values (@categoryid,'Search Engine')
		insert into SubCategory values (@categoryid,'Social Media')
		insert into SubCategory values (@categoryid,'Forum or Blog')
		insert into SubCategory values (@categoryid,'TV')
		insert into SubCategory values (@categoryid,'Newspaper')
		insert into SubCategory values (@categoryid,'Magazine')
		insert into SubCategory values (@categoryid,'Radio')
		insert into SubCategory values (@categoryid,'Friend / Family')
		insert into SubCategory values (@categoryid,'Event')
		insert into SubCategory values (@categoryid,'Flyer or Card')
		insert into SubCategory values (@categoryid,'Business / Supplier')
		insert into SubCategory values (@categoryid,'Case Manager')
		insert into SubCategory values (@categoryid,'Hospital Staff')
		insert into SubCategory values (@categoryid,'NDIS')
		insert into SubCategory values (@categoryid,'School')
		insert into SubCategory values (@categoryid,'Doctor / Medical Practitioner')
		insert into SubCategory values (@categoryid,'Care / Support Worker')
		insert into SubCategory values (@categoryid,'Other')

set @categoryname = 'CareType'
insert into Category values (@categoryname)
	select @categoryID = ID from Category where Name = @categoryname
		insert into SubCategory values (@categoryid,'Disability Support')
		insert into SubCategory values (@categoryid,'Aged Care')
		insert into SubCategory values (@categoryid,'Mental Health')
		insert into SubCategory values (@categoryid,'Post-Surgery')
		insert into SubCategory values (@categoryid,'Other')


set @categoryname = 'Relationship'
insert into Category values (@categoryname)
	select @categoryID = ID from Category where Name = @categoryname
		insert into SubCategory values (@categoryid,'Partner / Spouse')
		insert into SubCategory values (@categoryid,'Immediate Family')
		insert into SubCategory values (@categoryid,'Extended Family')
		insert into SubCategory values (@categoryid,'Legal Representative/ Advocate')
		insert into SubCategory values (@categoryid,'Approved Provider')
		insert into SubCategory values (@categoryid,'Care Co-ordinator')
		insert into SubCategory values (@categoryid,'Friend / Neighbour')

set @categoryname = 'Interest'
insert into Category values (@categoryname)
	select @categoryID = ID from Category where Name = @categoryname
		insert into SubCategory values (@categoryid,'Cooking')
		insert into SubCategory values (@categoryid,'Gardening')
		insert into SubCategory values (@categoryid,'Indoor Games / Puzzles')
		insert into SubCategory values (@categoryid,'Movies')
		insert into SubCategory values (@categoryid,'Music')
		insert into SubCategory values (@categoryid,'Cultural Festivals')
		insert into SubCategory values (@categoryid,'Pets')
		insert into SubCategory values (@categoryid,'Photography / Art')
		insert into SubCategory values (@categoryid,'Reading')
		insert into SubCategory values (@categoryid,'Sports')
		insert into SubCategory values (@categoryid,'Travel')
		insert into SubCategory values (@categoryid,'Working')
		insert into SubCategory values (@categoryid,'Other')

set @categoryname = 'Language'
insert into Category values (@categoryname)
	select @categoryID = ID from Category where Name = @categoryname
		insert into SubCategory values (@categoryid,'Arabic')
		insert into SubCategory values (@categoryid,'Cantonese')
		insert into SubCategory values (@categoryid,'Croatian')
		insert into SubCategory values (@categoryid,'English')
		insert into SubCategory values (@categoryid,'French')
		insert into SubCategory values (@categoryid,'German')
		insert into SubCategory values (@categoryid,'Greek')
		insert into SubCategory values (@categoryid,'Hebrew')
		insert into SubCategory values (@categoryid,'Hindi')
		insert into SubCategory values (@categoryid,'Hungarian')
		insert into SubCategory values (@categoryid,'Indonesian')
		insert into SubCategory values (@categoryid,'Italian')
		insert into SubCategory values (@categoryid,'Japanese')
		insert into SubCategory values (@categoryid,'Korean')
		insert into SubCategory values (@categoryid,'Mandarin')
		insert into SubCategory values (@categoryid,'Maltese')
		insert into SubCategory values (@categoryid,'Macedonian')
		insert into SubCategory values (@categoryid,'Netherlandic ')
		insert into SubCategory values (@categoryid,'Persian')
		insert into SubCategory values (@categoryid,'Polish')
		insert into SubCategory values (@categoryid,'Portugese ')
		insert into SubCategory values (@categoryid,'Russian')
		insert into SubCategory values (@categoryid,'Serbian')
		insert into SubCategory values (@categoryid,'Sinhalese')
		insert into SubCategory values (@categoryid,'Samoan ')
		insert into SubCategory values (@categoryid,'Spanish')
		insert into SubCategory values (@categoryid,'Tamil')
		insert into SubCategory values (@categoryid,'Tagalog (Filipino)')
		insert into SubCategory values (@categoryid,'Turkish')
		insert into SubCategory values (@categoryid,'Vietnamese')
		insert into SubCategory values (@categoryid,'Auslan (Australian sign Language)')
		insert into SubCategory values (@categoryid,'Other')

set @categoryname = 'Culture'
insert into Category values (@categoryname)
	select @categoryID = ID from Category where Name = @categoryname
		insert into SubCategory values (@categoryid,'Australian')
		insert into SubCategory values (@categoryid,'Australian Aboriginal')
		insert into SubCategory values (@categoryid,'Australian South East Islander')
		insert into SubCategory values (@categoryid,'Torres Strait Islander')
		insert into SubCategory values (@categoryid,'New Zealander')
		insert into SubCategory values (@categoryid,'Maori')
		insert into SubCategory values (@categoryid,'Polynesian')
		insert into SubCategory values (@categoryid,'Other Oceanian')
		insert into SubCategory values (@categoryid,'Western European')
		insert into SubCategory values (@categoryid,'Northern European')
		insert into SubCategory values (@categoryid,'Southern & Eastern European')
		insert into SubCategory values (@categoryid,'Middle Eastern')
		insert into SubCategory values (@categoryid,'Jewish')
		insert into SubCategory values (@categoryid,'Asian')
		insert into SubCategory values (@categoryid,'North American')
		insert into SubCategory values (@categoryid,'South American')
		insert into SubCategory values (@categoryid,'Central American')
		insert into SubCategory values (@categoryid,'Caribbean Islander')
		insert into SubCategory values (@categoryid,'South African')
		insert into SubCategory values (@categoryid,'Other African')
		insert into SubCategory values (@categoryid,'Other')


set @categoryname = 'Religion'
insert into Category values (@categoryname)
	select @categoryID = ID from Category where Name = @categoryname
		insert into SubCategory values (@categoryid,'Anglican')
		insert into SubCategory values (@categoryid,'Buddhist')
		insert into SubCategory values (@categoryid,'Catholic')
		insert into SubCategory values (@categoryid,'Christian – Other')
		insert into SubCategory values (@categoryid,'Greek Orthodox')
		insert into SubCategory values (@categoryid,'Hindu')
		insert into SubCategory values (@categoryid,'Islamic')
		insert into SubCategory values (@categoryid,'Jewish')
		insert into SubCategory values (@categoryid,'Presbyterian')
		insert into SubCategory values (@categoryid,'Russian Orthodox')
		insert into SubCategory values (@categoryid,'Sikh')
		insert into SubCategory values (@categoryid,'Spiritual')
		insert into SubCategory values (@categoryid,'Uniting Church ')
		insert into SubCategory values (@categoryid,'Other')
		insert into SubCategory values (@categoryid,'None')


set @categoryname = 'Pet'
insert into Category values (@categoryname)
	select @categoryID = ID from Category where Name = @categoryname
		insert into SubCategory values (@categoryid,'Dog')
		insert into SubCategory values (@categoryid,'Cat')
		insert into SubCategory values (@categoryid,'Other')

set @categoryname = 'MedicalLivingNeed'
insert into Category values (@categoryname)
	select @categoryID = ID from Category where Name = @categoryname
		insert into SubCategory values (@categoryid,'Cardiac / Vascular')
		insert into SubCategory values (@categoryid,'Diabetes / Cholesterol')
		insert into SubCategory values (@categoryid,'Incontinence (Bladder or Bowel)')
		insert into SubCategory values (@categoryid,'Memory Loss / Dementia')
		insert into SubCategory values (@categoryid,'Nutrition / Hydration')
		insert into SubCategory values (@categoryid,'Other Barriers or Concerns')
		insert into SubCategory values (@categoryid,'Physical / Mobility')
		insert into SubCategory values (@categoryid,'Complex communication ')
		insert into SubCategory values (@categoryid,'Psychological / Behavioral ')
		insert into SubCategory values (@categoryid,'Skin Integrity / Wound Management')
		insert into SubCategory values (@categoryid,'Speech / Swallowing')


set @categoryname = 'JobService'
insert into Category values (@categoryname)
	select @categoryID = ID from Category where Name = @categoryname
		insert into SubCategory values (@categoryid,'Companionship & Social Support')
		insert into SubCategory values (@categoryid,'Cleaning & Laundry')
		insert into SubCategory values (@categoryid,'Community Participation, Sports & Activities')
		insert into SubCategory values (@categoryid,'Showering, Toileting & Dressing')
		insert into SubCategory values (@categoryid,'Transport')
		insert into SubCategory values (@categoryid,'Manual Transfer & Mobility')
		insert into SubCategory values (@categoryid,'Independent Living Skills')
		insert into SubCategory values (@categoryid,'Assist with Self Medication')
		insert into SubCategory values (@categoryid,'Meal Preparation & Shopping ')
		insert into SubCategory values (@categoryid,'Nursing Services')


set @categoryname = 'Attachment'
insert into Category values (@categoryname)
	select @categoryID = ID from Category where Name = @categoryname
	 insert into SubCategory values (@categoryid,'Photo')
	 insert into SubCategory values (@categoryid,'Care Plan')
	 insert into SubCategory values (@categoryid,'NDIS approved Plan')
	 insert into SubCategory values (@categoryid,'GP Documents')
 	 insert into SubCategory values (@categoryid,'Birth Certificate')
	 insert into SubCategory values (@categoryid,'Medicare')
	 insert into SubCategory values (@categoryid,'Proof of Age')
	 insert into SubCategory values (@categoryid,'Psychology Report')
	 insert into SubCategory values (@categoryid,'Review and Assessment Reports')
