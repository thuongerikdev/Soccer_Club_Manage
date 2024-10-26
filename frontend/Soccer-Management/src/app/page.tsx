'use client'
import Link from "next/link"
import { Button } from "react-bootstrap"
require('dotenv').config()

const FirstPage = () => {

  
  return (
    <>
      <div className="red">
        <Link href={"/facebook"}> face book</Link>
      </div>
      <div className="red">
        <Link href={"/facebook"}> face book</Link>
      </div>
      <div className="red">
        <Link href={"/facebook"}> face book</Link>
      </div>
      <Button className="btn btn" onClick={() =>{console.log(process.env)}}>Sign In</Button>
      {/* <AppTable blogs={data?.sort((a: any , b: any) => b.id -a.id)}/> */}
     
    </>
  )
}
export default FirstPage