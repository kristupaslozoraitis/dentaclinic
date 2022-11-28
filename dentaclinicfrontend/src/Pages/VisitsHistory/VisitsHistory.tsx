import { Box, Button, Flex, Table, TableCaption, TableContainer, Tbody, Td, Tfoot, Th, Thead, Tr } from "@chakra-ui/react";
import React, { useEffect, useState } from "react";

interface CardType {
    id: number,
    name: string,
    surname: string,
}

const VisitsHistory = () => {
    const [test, setTest] = useState<CardType[]>([]);

    useEffect(() => {
        const dataFetch = async () => {
            const data = await (
                await fetch("https://dentaclinic20221015140303.azurewebsites.net/api/v1/patientsCards", {
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoib2RvbnRvQGdtYWlsLmNvbSIsImp0aSI6ImZiM2JhNWRmLWYzYzUtNGYyNC05MWU4LTMzN2RkNDQ4Y2Q2MCIsInN1YiI6IjQ1OWM4YWM3LTQ2YWUtNDBkZC04ZDBkLTBkZWJjOWM4ZjFmNiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6WyJPZG9udG9sb2dpc3QiLCJSZWdpc3RlcmVkVXNlciJdLCJleHAiOjE2Njg5NDg4ODcsImlzcyI6IktyaXN0dXBhcyIsImF1ZCI6IlRydXN0ZWRDbGllbnQifQ.9MPebzCjee2HY0RvXgNQlOMEdeq_ntnee6_Ss231SpQ'
                    }
                })
            ).json();
            console.log(data);
            setTest(data);
        };
        dataFetch();
    }, []);

    return (
        <>
            <TableContainer m={20}>
                <Table variant='simple'>
                    <Thead>
                        <Tr>
                            <Th>Apsilankymo data</Th>
                            <Th>Laikas</Th>
                            <Th>Paslauga</Th>
                            <Th>Odontologas</Th>
                            <Th></Th>
                        </Tr>
                    </Thead>
                    <Tbody>
                        <Tr>
                            <Td>2022-11-25</Td>
                            <Td>16:10</Td>
                            <Td>Paslauga tokia ir tokia</Td>
                            <Td>Vardas pavarde</Td>
                            <Td><Button>Atšaukti</Button></Td>
                        </Tr>
                        <Tr>
                            <Td>2022-11-25</Td>
                            <Td>16:10</Td>
                            <Td>Paslauga tokia ir tokia</Td>
                            <Td>Vardas pavarde</Td>
                            <Td>
                                <Flex gap={5}>
                                    <Button>Atšaukti</Button>
                                    <Button>Keisti laiką</Button>
                                </Flex>
                            </Td>
                        </Tr>
                        <Tr>
                            <Td>2022-11-25</Td>
                            <Td>16:10</Td>
                            <Td>Paslauga tokia ir tokia</Td>
                            <Td>Vardas pavarde</Td>
                            <Td><Button>Atšaukti</Button></Td>
                        </Tr>
                    </Tbody>
                    <Tfoot>
                        <Tr>
                            <Td>2022-11-25</Td>
                            <Td>16:10</Td>
                            <Td>Paslauga tokia ir tokia</Td>
                            <Td>Vardas pavarde</Td>
                            <Td><Button>Atšaukti</Button></Td>
                        </Tr>
                    </Tfoot>
                </Table>
            </TableContainer>
        </>
    )
}

export { VisitsHistory }