import React from 'react'
import usePayments from '../hooks/usePayments'
import { getPaymentsByDateRange } from '../services/paymentService';
import moment from 'moment';

function ViewPayment() {
  const {state, dispatch, pagination, setPagination, patient,dateRange, setDateRange} = usePayments();

  const handleChange = (e) => {
    setDateRange({...dateRange, [e.target.name]: moment(e.target.value).format("YYYY-MM-DD")})
  }
  const getDateRangePayment = async(e) => {
    e.preventDefault();
    var res = await getPaymentsByDateRange(patient.id, pagination, dateRange);
    dispatch({type: "all-patient-payments", payments: res?.payment})
  }
  return (
    <div>
        <h5>All Patients</h5>
        <div>
            <form onSubmit={getDateRangePayment}>
                <h5>Search By Date</h5>
                <div className="form-group">
                    <label htmlFor="">Start Date:</label>
                    <input type="date" name='StartDate' onChange={handleChange}/>
                </div>
                <div className="form-group">
                    <label htmlFor="">End Date:</label>
                    <input type="date" name='EndDate' onChange={handleChange}/>
                </div>
                <div className="form-group">
                    <input type="submit"/>
                </div>
            </form>
        </div>
        <div>
            <h5>Name: </h5>
            <p>{patient?.firstName + " " + patient?.lastName}</p>
            <h5>Patient Id: </h5>
            <p>{patient?.id}</p>
        </div>
        <table>
            <thead>
                <tr>
                    <th>Amount</th>
                    <th>Payment Date</th>
                </tr>
            </thead>
            <tbody>
                {state?.payments?.map((payment,i) => 
                    <tr key={i}>
                        <td>{payment?.amount}</td>
                        <td>{payment?.payment_date}</td>
                    </tr>
                )}
            </tbody>
        </table>
        {pagination?.totalPage > 1 &&
            <ul style={{display:"flex", gap:"5px", listStyle: "none", padding: 0}}>
                {(new Array(pagination?.totalPage).fill(2)).map((x,i) => 
                    <li style={{cursor:"pointer"}} key={i} onClick={() => setPagination({...pagination, currentPage: i+1})}>{i+1}</li>
                    )}
            </ul>
        }
    </div>
  )
}

export default ViewPayment