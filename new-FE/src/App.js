import {Routes, Route} from "react-router-dom"
import './App.css';
import Register from "./pages/Register";
import Home from "./pages/Home";
import Payment from "./pages/Payment";
import Navbar from "./component/Navbar";
import Patients from "./pages/Patients";
import ViewPayment from "./pages/ViewPayment";

function App() {
  return (
    <div>
      <Navbar/>
      <Routes>
        <Route path="/" element={<Home/>} />
        <Route path="/register" element={<Register/>} />
        <Route path="/payment/:id" element={<Payment/>} />
        <Route path="/payment/:id/view" element={<ViewPayment/>} />
        <Route path="/patients" element={<Patients/>} />
      </Routes>
    </div>
  );
}

export default App;
