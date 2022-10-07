import { Outlet } from "react-router";
import { Navigation } from "./components/Navigation";

function App() {
  return (
    <>
      <div className="h-screen flex flex-col">
        <Navigation />
        <Outlet />
      </div>
    </>
  );
}

export default App;
