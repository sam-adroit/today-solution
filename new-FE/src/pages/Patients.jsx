import React from 'react'
// import SearchField from '../component/SearchField'
import usePatients from '../hooks/usePatients'
import { Link } from 'react-router-dom';

function Patients() {
  const {state,pagination, setPagination} = usePatients();
  return (
    <div>
        {/* <SearchField byName={true}/> */}
        <h5>All Patients</h5>
        <table>
            <thead>
                <tr>
                    <th>Patient Name</th>
                    <th>Patient Id</th>
                    <th>Patient Email</th>
                    <th>Balance</th>
                    <th>Last Payment Date</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
                {state?.patients?.map((patient,i) => 
                    <tr key={i}>
                        <td>{patient?.firstName + " " +patient?.lastName}</td>
                        <td>{patient?.id}</td>
                        <td>{patient?.email}</td>
                        <td>{patient?.balance}</td>
                        <td>{patient?.lastPayment}</td>
                        <td>
                            <Link to={`/payment/${patient?.id}`}>Make Payment</Link>
                            <span>{" "}</span>
                            <Link to={`/payment/${patient?.id}/view`}>View All Payments</Link>
                        </td>
                    </tr>
                )}
            </tbody>
        </table>
        <ul style={{display:"flex", gap:"5px", listStyle: "none", padding: 0}}>
            {(new Array(pagination?.totalPage).fill(2)).map((x,i) => 
                <li style={{cursor:"pointer"}} key={i} onClick={() => setPagination({...pagination, currentPage: i+1})}>{i+1}</li>
            )}
        </ul>
    </div>
  )
}

export default Patients