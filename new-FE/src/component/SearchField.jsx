import React, {useContext, useState} from 'react'
import { getPatientByEmail, getPatientById, getPatientsByName } from '../services/patientService';
import { PatientContext, PatientsContext } from '../context/Context';

function SearchField({byName}) {
  const[searchBy, setSearchBy] = useState("");
  const[searchStr, setSearchStr] = useState("");
  const {dispatch} = useContext(PatientContext)
  const {dispatch:fn} = useContext(PatientsContext)
  const handleSearch = async (e) => {
    e.preventDefault()
    if(byName) handlePatients()
    else handlePatient()
  }
  const handlePatient = async() => {
    let res = null
    if(searchBy === "id"){
        res = await getPatientById(searchStr);
    }
    if(searchBy === "email"){
        res = await getPatientByEmail(searchStr);
    }
    dispatch({type: "get-patient", patient: res})
  }
  const handlePatients = async() => {
    let res = null;
    if(searchBy === "id"){
        res = [await getPatientById(searchStr)];
    }
    if(searchBy === "email"){
        res = [await getPatientByEmail(searchStr)];
    }
    if(searchBy === "name"){
        res = await getPatientsByName(searchStr);
    }    
    fn({type: "all-patient", patients: res})
  }
  return (
    <div className="search-field">
        <label htmlFor="">
            <input type="checkbox" name="" id="" checked={searchBy === "id"}  onChange={() => setSearchBy("id")}/>
            <span>Search Patient by Id</span>
        </label>
        <label htmlFor="">
            <input type="checkbox" name="" id="" checked={searchBy === "email"} onChange={() => setSearchBy("email")}/>
            <span>Search Patient by Email</span>
        </label>
        {byName &&
            <label htmlFor="">
                <input type="checkbox" name="" id="" checked={searchBy === "name"} onChange={() =>setSearchBy("name")}/>
                <span>Search Patient by Name</span>
            </label>
        }
        {!!searchBy &&
            <div className="search-input">
                <form action="" onSubmit={handleSearch}>
                    <input type='text' onChange={e => setSearchStr(e.target.value)} value={searchStr} placeholder={`Search patient(s) by ${searchBy || 'id'}`}/>
                    <input type="submit" value={'search'}/>
                </form>
            </div>
        }
    </div>
  )
}

export default SearchField