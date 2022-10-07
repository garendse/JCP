describe("Login", () => {
  beforeEach(() => {
    cy.visit("/login");
  });

  it("Redirects to login", () => {
    cy.visit("/");
    cy.url().should("contain", "/login");
  });

  it("Wrong Username", () => {
    cy.get("#username").type("Username");
    cy.get("#password").type("Debug");
    cy.get("#loginform").submit();
    cy.get("#error")
      .should("exist")
      .should("contain.text", "Invalid username or password");
  });

  it("Wrong Password", () => {
    cy.get("#username").type("cbsupdbg");
    cy.get("#password").type("Password");
    cy.get("#loginform").submit();
    cy.get("#error")
      .should("exist")
      .should("contain.text", "Invalid username or password");
  });

  it("Right creds", () => {
    cy.get("#username").type("cbsupdbg");
    cy.get("#password").type("Carma");
    cy.get("#loginform").submit();
    cy.url().should("not.contain", "login");
  });
});
