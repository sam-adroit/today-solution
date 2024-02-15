import React, { useState } from 'react';
import {useNavigate} from "react-router-dom"
import { addPatient } from '../services/patientService';
import moment from 'moment';

function Register() {
  let initVal = {firstName: "", lastName: "", email: "", created_on: moment().format()};
  const [patient, setPatient] = useState(initVal);
  const navigate = useNavigate();
  const handleSubmit = (e) => {
    setPatient({...patient, [e.target.name]: e.target.value});
  }
  const addPatientFn = async(e) => {
    e.preventDefault();
    await addPatient(patient);
    navigate("/")
  }
  return (
    <div>
       <h5>Fill New Patient details</h5>
       <form onSubmit={addPatientFn}>
            <div className="form-group">
                <label htmlFor="">FirstName: </label>
                <input type="text" name='firstName' onChange={handleSubmit} value={patient.firstName} placeholder='Your FirstName'/>
            </div>
            <div className="form-group">
                <label htmlFor="">LasName: </label>
                <input type="text" name='lastName' onChange={handleSubmit} value={patient.lastName} placeholder='Your Last Name'/>
            </div>
            <div className="form-group">
                <label htmlFor="">Email</label>
                <input type="email" name='email' onChange={handleSubmit} value={patient.email} placeholder='someone@email.com'/>
            </div>
            <div className="form-group">
                <input type="submit" />
            </div>
       </form> 
    </div>
  )
}

export default Register