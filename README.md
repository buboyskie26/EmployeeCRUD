# Simple Employee CRUD Project

## Overview

This project demonstrates a simple CRUD (Create, Read, Update, Delete) application for managing employee data. It is built using the .NET Framework 4.8, with Visual Studio as the integrated development environment (IDE), and MS SQL as the database.

## Technologies Used

- **.NET Framework**: 4.8
- **IDE**: Visual Studio
- **Database**: MS SQL

## Database Schema

The project uses a single table called `Employee` with the following fields:

- **ID**: Primary key
- **EmpNo**: Unique, alphanumeric 6 characters
- **FirstName**: Alphabetic, up to 15 characters
- **LastName**: Alphabetic, up to 15 characters
- **Birthdate**: Date
- **ContactNo**: Numeric, 11 characters, must start with 09
- **EmailAddress**: String

## Business Rules

- `EmpNo` should be unique.
- No duplicate combination of `FirstName` and `LastName`.
- Limit `FirstName` and `LastName` input to 15 characters each.
- Limit `ContactNo` input to 11 characters, must start with 09.
- Handle possible exceptions gracefully.

## Implementation Details

### ORM (Object-Relational Mapping)

#### Entity Framework
