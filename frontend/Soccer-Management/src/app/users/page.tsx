'use client'
import Link from "next/link";
import UserTable from "@/components/table/app.usertable";
import useSWR from 'swr';
import { useRouter } from 'next/navigation';
require('dotenv').config()
const UserPage = () => {
    const router = useRouter()
    const fetcher = (url: string) => fetch(url).then((res) => res.json());

    const { data, error ,isLoading} = useSWR(
        process.env.NEXT_PUBLIC_USER_GETALL,
        fetcher,
        {
            revalidateIfStale: false,
            revalidateOnFocus: false,
            revalidateOnReconnect: false
        }
    );

    if (error) {
        return <div>Error loading user data.</div>;
    }

    if (!data) {
        return <div>Loading...</div>; // Use this to indicate loading state
    }

    return (
        <>
            <UserTable users ={data.sort((a: any, b: any) => b.userId - a.userId)} />
        </>
    );
}

export default UserPage;