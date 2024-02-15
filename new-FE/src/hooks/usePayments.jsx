import {useEffect, useContext, useState} from 'react'
import {useParams, useNavigate} from "react-router-dom"
import {  PaymentContext } from '../context/Context';
import { getPaymentsByUserId } from '../services/paymentService';
import moment from 'moment';

function usePayments() {
    const {id} = useParams();
    const navigate = useNavigate();
    const {state, dispatch} = useContext(PaymentContext)
    const [pagination, setPagination] = useState({currentPage: 1, itemPerPage: 5, totalPage: 0})
    const [patient, setPatient] = useState(null);
    const intialDateRange = {StartDate: moment().format("YYYY-MM-DD"), EndDate: moment().format("YYYY-MM-DD")}
    const [dateRange, setDateRange] = useState(intialDateRange);
    
    useEffect(() => {
      if(!!!id) navigate("/")
      const fetchPayment = async() => {
        const res = await getPaymentsByUserId(id, {currentPage: pagination.currentPage,itemPerPage: pagination.itemPerPage});
        dispatch({type: "all-patient-payments", payments: res?.payment})
        setPagination(res.pagination);
        setPatient(res.patient);
      }
      fetchPayment();
    },[id,pagination.currentPage, pagination.itemPerPage, dispatch, navigate])
  return (
    {state, dispatch, pagination, setPagination, patient,dateRange, setDateRange }
  )
}

export default usePayments