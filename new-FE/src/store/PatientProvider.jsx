import React, { useReducer } from 'react'
import { PatientContext } from '../context/Context'
import { patientsReducer } from './patientsReducer';

function PatientProvider({children}) {
  const initialState = {loading: false, error: null, isError: false, patient: null};
  const [state, dispatch] = useReducer(patientsReducer, initialState)
  return (
    <PatientContext.Provider value={{state, dispatch}}>
        {children}
    </PatientContext.Provider>
  )
}

export default PatientProvider