import moment from 'moment';
import React, { useEffect, useState } from 'react';
import {useParams, useNavigate} from "react-router-dom";
import { addPayment } from '../services/paymentService';

function Payment() {
  const navigate = useNavigate();
  const {id, name} = useParams()
  const payObj = {patient_Id: id, amount: 0, payment_date: moment().format()}
  const [payment, setPayment] = useState(payObj);
  useEffect(() => {
    if(!!!id) navigate("/");
  }, [id, navigate])
  const addPaymentFn = async (e) => {
    e.preventDefault();
    await addPayment(payment);
    navigate(`/payment/${id}/view`)
  }
  return (
    <div>
        <h5>Make a payment For : {name}</h5>
        <form onSubmit={addPaymentFn}>
            <div className="form-group">
                <input type="text" value={id} placeholder='Your Email' hidden/>
            </div>
            <div className="form-group">
                <label htmlFor="">Amount: </label>
                <input type="number" name='amount' onChange={e => setPayment({...payment, [e.target.name]: e.target.value})}/>
            </div>
            <div className="form-group">
                <input type="submit"/>
            </div>
        </form>
    </div>
  )
}

export default Payment