import { Box, Container, Flex, Heading } from "@chakra-ui/react"
import React, { FunctionComponent } from "react"
import { FreeVisitsView } from "./Pages/FreeVisitsView"

const HomePage: FunctionComponent = () => {
    return (
        <Box m={5}>
            <Flex justifyContent="center">
                <Heading>
                    Denta Clinic
                </Heading>
            </Flex>
            <Box>
                <FreeVisitsView />
            </Box>
        </Box>
    )
}

export { HomePage }