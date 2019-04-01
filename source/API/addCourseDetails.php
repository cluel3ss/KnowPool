<?php
/**
 * Created by PhpStorm.
 * User: Xonshiz
 * Date: 02-11-2018
 * Time: 10:48 PM
 */

if ($_SERVER['REQUEST_METHOD'] === 'POST')
{
    require_once('libs/initialize.php');
    /*
     * Open a DB connection and start checking the fields whether they are empty or not.
     * If they're empty, return appropriate error.
     * Also, if the field is NOT empty and we're using that data, we need to escape the special chars to avoid SQL Injection.
     */
    $db = db_connect();

    $cd_a_id = mysqli_real_escape_string($db, $_POST['cd_a_id']);
    $cd_c_id = mysqli_real_escape_string($db, $_POST['cd_c_id']);
    $cd_title = mysqli_real_escape_string($db, $_POST['cd_title']);
    $cd_keywords = mysqli_real_escape_string($db, $_POST['cd_keywords']);
    $cd_desc = mysqli_real_escape_string($db, $_POST['cd_desc']);

    $query = "INSERT INTO courseDetails (cd_a_id, cd_c_id, cd_title, cd_keywords, cd_desc) VALUES ('$cd_a_id', '$cd_c_id', '$cd_title','$cd_keywords', '$cd_desc')";
    // echo $query;
    $result = mysqli_query($db, $query);


    if ($result)
    {
        echo json_encode(array(
            'error_code' => '0',
            'message' => 'Successfully Added'
        ));
        db_disconnect($db, $result);
        exit(0);
    }
    else
    {
        echo json_encode(array(
            'error_code' => '1',
            'message' => 'Failed To Add'
        ));
        db_disconnect($db, $result);
        exit(1);
    }
}
else
{
    echo "<html><body><h1>Method Not Allowed</h1></body></html>";
}

?>

