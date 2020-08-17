CREATE OR REPLACE VIEW patient_list_v AS 
SELECT
per.person_id AS 'USER_ID',
pat.patient_id AS 'PATIENT_ID',
per.first_name AS 'FIRST_NAME',
per.last_name AS 'LAST_NAME',
per.dob AS 'DOB',
per.gender AS 'GENDER',
addr.street_address AS 'STREET_ADDRESS',
addr.city AS 'CITY',
addr.state AS 'STATE',
addr.postal_code AS 'ZIP_CODE',
per.phone AS 'PHONE'
FROM person AS per, patient AS pat, address AS addr
WHERE per.person_id = pat.person_id AND per.address_id = addr.address_id;
    
SELECT * FROM patient_list_v;