describe("checkin", () => {
  beforeEach(() => {
    cy.login();
    cy.visit("/checkin");
  });

  it("Existing", () => {
    cy.get("#search").type("0749422266");
    cy.get("#findButton").click();
    cy.get(".mb-2 > .m-2").click();
    cy.get(".bg-green-300").click();
    cy.get(".h-screen > :nth-child(3) > .text-lg").should(
      "contain.text",
      "Vehicle"
    );
    cy.get(":nth-child(3) > .mb-2").select(2);
    cy.get("#odometer").type("1234");
    cy.get(".bg-green-300").click();
    cy.get(".text-2xl > :nth-child(1)").should("contain.text", "Ro Number");
  });
});
