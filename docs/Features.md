# Requirements & progress

-   Main Quotation interaction

    -   Core Logic

        -   [x] Add Quotes
        -   [x] Edit Quote Info (status & technician)
        -   [x] Add Job to quote by code & lookup action
        -   [ ] Quotes should only be allowed to be edited on one device at a time. The proper way to do it is with websockets & non persistent storage. Another option is to make the frontend make a request every 5sec or so with the quote_id and before another client loads the quote check if the last timestamp stored is older than 10sec and show it if so, otherwise block it and check every x sec until the other client stopped updating the timestamp.
        -   [ ] Auto quote from supplier (Email /whatsapp)

            -   Would be too complex to implement as OCR is needed to read quotes from supplier, consider alternatives or outsource this part to external system. Rolling own will need quite a bit of work, look into tesseract OCR and setup templates with OpenCV - Try doing it in python or C++, as the libraries available are better. See [Here](https://github.com/SuperUserDone/OpenParkingManager/blob/e7318385dc70db4f56731186e0ab07e3f55d5df5/common/src/ReadLicensePlates.cpp#L45) (License plate recognition I've written for a competition in 2019 HORRIFIC code but should be suitable to get an idea of the process involved)
            -   Alternative is a system simmilar like how Youtube does ads

                1. User clicks to watch a video
                2. System sends an event to ad provider systems and they have 80 ms to respond to the request with a named price & a ad they want to show
                3. Youtube picks highest bidder & shows their ad

                Adapted for JCP wil be something like (JUST AN IDEA!! Needs to be **PROPERLY** thought through & discussed)

                4. User adds parts to quote & timeframe they require parts
                5. Part request gets posted on a system suppliers have access to
                6. Representative of supplier enters a price and qty they can provide & estimated lead time if ordered by some date/time & date offer expires (Try to prevent abuse by suppliers by penalising incorrect lead times w/o some condition being met eg aggreed increase of qty etc)
                7. User selects the parts from supplier based on some metric (Eg price, all from some supplier or time delivered distance from user)
                8. User places order & does payment stuff of selected parts
                9. Supplier supplies the user the parts

-   Logical Isolation
    -   [x] Split centers
    -   [x] Dont share data between centres
    -   [ ] User permissions
        -   Ask to clarify permission system no info exchanged at this stage on what roles users should have & their permissions
-   Maintenance and admin screens
    -   [x] Add
    -   [x] Edit
    -   [x] Don't send passwords to frontend

# Production Ready checklist

-   [x] Confirm deployment is possible on IIS:
        [Yes](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/?view=aspnetcore-6.0)
-   [ ] Password Hashing
-   [ ] CORS
-   [ ] CSRF Tokens/Other form of CSRF protection
-   [ ] Check for IDOR issues
-   [x] Validation that data fits in db types
-   [x] Error cheking
    -   Note: Needs to be tested properly

# Cleanup needed

-   [CheckIn.tsx](../../../../../Code/JCP/jcpfrontend/src/routes/CheckIn.tsx) Works but is messy. Low Priority
