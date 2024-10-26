'use client'
import Link from "next/link";
import UserTable from "@/components/table/app.usertable";
import useSWR from 'swr';

const UserPage = () => {
    const fetcher = (url: string) => fetch(url).then((res) => res.json());
    const { data, error ,isLoading} = useSWR(
        "http://localhost:3001/api/auth/getall",
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
            <UserTable user={data.sort((a: any, b: any) => b.userId - a.userId)} />
        </>
    );
}

export default UserPage;