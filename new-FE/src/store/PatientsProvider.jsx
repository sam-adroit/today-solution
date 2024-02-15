import React, {useReducer } from 'react';
import { patientsReducer } from './patientsReducer';
import { PatientsContext } from '../context/Context';

function PatientsProvider({children}) {
  const initialState = {loading: false, error: null, isError: false, patients: []}
  const [state, dispatch] = useReducer(patientsReducer,initialState)
  return (
    <PatientsContext.Provider value={{state, dispatch}}>
      {children}
    </PatientsContext.Provider>
  )
}


export default PatientsProvider