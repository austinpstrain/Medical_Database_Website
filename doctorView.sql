CREATE OR REPLACE VIEW doctor_list_v AS 
SELECT
per.person_id AS 'USER_ID',
doc.doctor_id AS 'DOCTOR_ID',
specs.type AS 'SPECIALIZATION',
per.first_name AS 'FIRST_NAME',
per.last_name AS 'LAST_NAME',
per.dob AS 'DOB',
per.gender AS 'GENDER',
addr.street_address AS 'STREET_ADDRESS',
addr.city AS 'CITY',
addr.state AS 'STATE',
addr.postal_code AS 'ZIP_CODE',
per.phone AS 'PHONE'
FROM person AS per, doctor AS doc, address AS addr, specializations AS specs
WHERE per.person_id = doc.person_id AND per.address_id = addr.address_id AND doc.specialization_id = specs.specialization_id;
    
SELECT * FROM doctor_list_v;