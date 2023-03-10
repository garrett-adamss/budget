CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';


--paychecks BIGINT
CREATE TABLE IF NOT EXISTS keeps(
id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
creatorId VARCHAR(255) NOT NULL,
name VARCHAR(255) NOT NULL,
description TEXT NOT NULL,
img VARCHAR(255) NOT NULL,
views INT NOT NULL DEFAULT 0,
kept VARCHAR(255) NOT NULL DEFAULT 0,

FOREIGN KEY (creatorId) REFERENCES accounts(id)
) default charset utf8 COMMENT '';

DROP TABLE paycheck;

CREATE TABLE paycheck (
    id int NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'Primary Key',
    accountId VARCHAR(255) NOT NULL COMMENT 'accountId',
    create_time DATETIME COMMENT 'Create Time',
    paycheckDate DATE NOT NULL COMMENT 'Paycheck Date',
    grossIncome DECIMAL(18,2) NOT NULL COMMENT 'Gross Income',
    taxAmount DECIMAL(18,2) NOT NULL COMMENT 'Tax Amount',
    netAmount DECIMAL(18,2) NOT NULL COMMENT 'Net Amount',
    savings DECIMAL(18,2) NOT NULL COMMENT 'Savings',
    tithe DECIMAL(18,2) NOT NULL COMMENT 'Tithe',
    payPeriodStartDate DATE NOT NULL COMMENT 'Pay Period Start Date',
    payPeriodEndDate DATE NOT NULL COMMENT 'Pay Period End Date'

);