import React from 'react'
import { Link } from 'react-router-dom'

function Navbar() {
  return (
    <div>
        <ul>
            <li><Link to="/">Home</Link></li>
            <li><Link to="/register">Register Patient</Link></li>
            <li><Link to="/patients">Patients</Link></li>
        </ul>
    </div>
  )
}

export default Navbar