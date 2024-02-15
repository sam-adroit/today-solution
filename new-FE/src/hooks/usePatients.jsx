import {useEffect, useContext, useState} from 'react'
import { PatientsContext } from '../context/Context';
import { getAllPatients } from '../services/patientService';

function usePatients() {
    const {state, dispatch} = useContext(PatientsContext)
    const [pagination, setPagination] = useState({currentPage: 1, itemPerPage: 5, totalPage: 0})
    useEffect(() => {
      const fetchPatients = async() => {
        const res = await getAllPatients(pagination.currentPage, pagination.itemPerPage);
        dispatch({type: "all-patient", patients: res?.response})
        setPagination(res.pagination);
        //dispatch({type: "update-pagination", action: {pagination: {...state.pagination, totalPage: }}})
      }
        fetchPatients();
    },[dispatch, pagination.currentPage, pagination.itemPerPage])
  return (
    {state, pagination, setPagination}
  )
}

export default usePatients