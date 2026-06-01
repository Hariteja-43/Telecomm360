# TODO

- [x] Inspect current service/repository methods for GetAll* signatures and filtering behavior.
- [x] Update Controllers: change every GET-all endpoint to accept `SearchDto` via query params and forward to service.


- [ ] Update Services interfaces + implementations to include `GetAllX(SearchDto search)` and pass criteria to repository.
- [ ] Update Repositories interfaces + implementations to implement filtering using `SearchDto`.
- [ ] Ensure InvoiceController GetAll actually uses `SearchDto`.
- [ ] Run `dotnet build` to verify compilation.

